import React, { useState, useEffect } from "react";
import { TextField, Button } from "@mui/material";
import api, { API_ROUTES } from "../../services/api";
import './style.css';

const Account = () => {
  const [name, setName] = useState('');
  const [lastName, setLastName] = useState('');
  const [email, setEmail] = useState('');
  const [phone, setPhone] = useState('');

  useEffect(() => {
    const loadOwnAccount = async () => {
      const response = await api.get(API_ROUTES.getOwnAccount);
      const account = response.data;
      setName(account.name);
      setLastName(account.lastName);
      setEmail(account.email);
      setPhone(account.phone ? account.phone : '');
    };

    loadOwnAccount();
  }, []);  

  const save = async (event) => {
    event.preventDefault();
    const updatedAccount = { name, lastName, email, phone };
    await api.put(API_ROUTES.saveAccount, updatedAccount);
  };

  return (
    <form onSubmit={save} className="account-form">
      <TextField
        required
        label="Name"
        variant="outlined"
        fullWidth
        value={name}
        onChange={e => setName(e.target.value)}
        className="account-form-control"
      />
      <TextField
        required
        label="Last name"
        variant="outlined"
        fullWidth
        value={lastName}
        onChange={e => setLastName(e.target.value)}
        className="account-form-control"
      />
      <TextField
        required
        label="Email"
        variant="outlined"
        fullWidth
        value={email}
        onChange={e => setEmail(e.target.value)}
        className="account-form-control"
      />
      <TextField
        label="Phone"
        variant="outlined"
        fullWidth
        value={phone}
        onChange={e => setPhone(e.target.value)}
        className="account-form-control"
      />
      <Button
        type="submit"
        variant="contained"
        onClick={save}
        fullWidth
      >
        Save
      </Button>
    </form>
  );
};

export default Account;