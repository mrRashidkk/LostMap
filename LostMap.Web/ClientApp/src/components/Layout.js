import React from 'react';
import Header from './header';
import { LAYOUT } from '../constants';

export default function Layout({ children }) {
  return (
    <>
      <Header />
      <div className='main-container' style={{ paddingTop: LAYOUT.HeaderHeight }}>
        {children}
      </div>
    </>
  );
}