import axios from 'axios';

const api = axios.create({});

export const API_ROUTES = {
  login: 'api/auth/login',
  recreateTokens: 'api/auth/recreateTokens',
  signUp: 'api/auth/signUp',
  logout: 'api/auth/logout',
  getOwnAccount: 'api/account/getOwn',
  saveAccount: 'api/account/save',
  saveFinding: 'api/finding/save',
  getAllFindings: 'api/finding/getAll',
  getFinding: 'api/finding/get',
  saveLoss: 'api/loss/save',
  getAllLosses: 'api/loss/getAll',
  getLoss: 'api/loss/get'
};

export default api;