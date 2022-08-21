import React, { useState, useEffect } from 'react';
import { Route, Routes } from 'react-router-dom';
import AppRoutes from './AppRoutes';
import Layout from './components/Layout';
import './custom.css';
import { AuthProvider } from './services/auth-context';
import AuthService from './services/auth-service';
import AuthPage from './components/auth-page';

const authService = new AuthService();

export default function App() {
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {    
    const onAuthStatusChange = () => {
      setIsAuthenticated(authService.isAuthenticated);
    }

    const initAuth = async () => {
      await authService.recreateTokens();
      setIsAuthenticated(authService.isAuthenticated);
      setIsLoading(false);
    };

    initAuth();
    authService.subscribeOnStatusChange(onAuthStatusChange);
  }, []);

  return (
    <AuthProvider value={authService}>
      {isAuthenticated &&
        <Layout>
          <Routes>
            {AppRoutes.map((route, index) => {
              const { element, ...rest } = route;
              return <Route key={index} {...rest} element={element} />;
            })}
          </Routes>
        </Layout>
      }
      {!isAuthenticated && !isLoading && <AuthPage />}
    </AuthProvider>
  );  
}
