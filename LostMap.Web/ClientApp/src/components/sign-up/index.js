import React, { useState } from "react";
import { TextField, Button } from '@mui/material';
import { useAuth } from "../../services/auth-context";
import "./style.css";

export default function SignUp() {
  const [email, setEmail] = useState('');
  const [name, setName] = useState('');
  const [lastName, setLastName] = useState('');
  const [password, setPassword] = useState('');
  const [errors, setErrors] = useState({ 
    email: false, name: false, lastName: false, password: false });

  const authService = useAuth();

  const submit = async (event) => {
    event.preventDefault();
    if (validateForm()) {
      const formData = new FormData();
      formData.append('signUpRequest.email', email);
      formData.append('signUpRequest.name', name);
      formData.append('signUpRequest.lastName', lastName);
      formData.append('signUpRequest.password', password);
      await authService.signUp(formData);
    }    
  };

  const validateForm = () => {
    const newErrorState = {
      email: !email,
      name: !name,
      lastName: !lastName,
      password: !password
    };
    setErrors(newErrorState);
    return !Object.values(newErrorState).some(isError => isError);
  };

  return (
    <form onSubmit={submit}>
      <div className="sign-up-control">
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
      <div className="sign-up-control">
        <TextField 
          required
          label="Name" 
          variant="outlined"
          fullWidth
          value={name}
          onChange={e => setName(e.target.value)}
          onBlur={validateForm}
          error={errors.name}
        />
      </div>
      <div className="sign-up-control">
        <TextField 
          required
          label="Last name" 
          variant="outlined"
          fullWidth
          value={lastName}
          onChange={e => setLastName(e.target.value)}
          onBlur={validateForm}
          error={errors.lastName}
        />
      </div>
      <div className="sign-up-control">
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
      <div className="sign-up-control">
        <Button
          type="submit"
          variant="contained"
          fullWidth
        >Sign up</Button>
      </div>      
    </form>
  );
}