import React from "react";
import { AppBar, Toolbar, Button } from "@mui/material";
import { Link } from "react-router-dom";
import { PATHS } from "../../AppRoutes";
import { LAYOUT } from '../../constants';
import { useAuth } from "../../services/auth-context";
import './style.css';

export default function Header() {
  const authService = useAuth();

  return (
    <AppBar position='fixed' style={{ height: LAYOUT.HeaderHeight }}>
      <Toolbar>
        <Link className='nav-link' to={PATHS.Home}>Home</Link>
        <Link className='nav-link' to={PATHS.Account}>Account</Link>
        <Link className='nav-link' to={PATHS.Map}>Map</Link>
        <Button 
          onClick={() => authService.logout()}
          variant='contained'
          color='info'
        >Logout</Button>
      </Toolbar>
    </AppBar>
  );
}