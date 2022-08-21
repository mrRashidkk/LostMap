import React, { useState } from 'react';
import { Paper, Button } from '@mui/material';
import Login from '../login';
import SignUp from '../sign-up';
import './style.css';

const TABS = {
  Login: 0,
  SignUp: 1
};

export default function AuthPage() {
  const [tab, setTab] = useState(TABS.Login);

  return (
    <Paper className="auth-form-container">      
      { tab === TABS.Login && <Login />}
      { tab === TABS.SignUp && <SignUp />}
      <div className="auth-form-switch">
        { tab === TABS.Login 
          ? <>
              New to LostMap? 
              <Button 
                onClick={() => setTab(TABS.SignUp)}
              >Sign up</Button>
            </>
          : <>
              Already have an account? 
              <Button 
                onClick={() => setTab(TABS.Login)}
              >Login</Button>
            </>
        }
      </div>            
    </Paper>
  );
}