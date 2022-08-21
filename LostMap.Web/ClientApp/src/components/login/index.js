import React, { useState } from "react";
import { TextField, Button } from '@mui/material';
import { useAuth } from "../../services/auth-context";
import './style.css';

export default function Login() {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [errors, setErrors] = useState({ email: false, password: false });

  const authService = useAuth();

  const submit = async (event) => {
    event.preventDefault();
    if (validateForm()) {
      await authService.login(email, password);
    }        
  };

  const validateForm = () => {
    const newErrorState = {
      email: !email,
      password: !password
    };
    setErrors(newErrorState);
    return !Object.values(newErrorState).some(isError => isError);
  };

  return (
    <form onSubmit={submit}>
      <div className="login-control">
        <TextField 
          required
          label="Email" 
          variant="outlined"
          fullWidth
          value={email}
          onChange={e => setEmail(e.target.value)}
          onBlur={validateForm}
          error={errors.email}
        />        
      </div>
      <div className="login-control">
        <TextField 
          required
          label="Password"
          type="password"
          autoComplete="current-password"
          variant="outlined"
          fullWidth
          value={password}
          onChange={e => setPassword(e.target.value)}
          onBlur={validateForm}
          error={errors.password}
        />
      </div>
      <div className="login-control">
        <Button
          type="submit"
          variant="contained"
          fullWidth
        >Login</Button>
      </div>      
    </form>
  );
}