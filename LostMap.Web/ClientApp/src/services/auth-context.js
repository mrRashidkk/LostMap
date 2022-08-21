import React, { useContext } from 'react';

const AuthContext = React.createContext();

export const AuthProvider = AuthContext.Provider;

export const useAuth = () => useContext(AuthContext);