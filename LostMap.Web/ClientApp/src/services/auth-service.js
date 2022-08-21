import api, { API_ROUTES } from "./api";

const saveAuthData = (authData) => {
  localStorage.setItem('email', authData.email);
  localStorage.setItem('accessToken', authData.accessToken);
  localStorage.setItem('refreshToken', authData.refreshToken);
};

const clearAuthData = () => {
  localStorage.removeItem('email');
  localStorage.removeItem('accessToken');
  localStorage.removeItem('refreshToken');
};

const TOKEN_RECREATION_INTERVAL_MS = 1500000;

export default class AuthService {
  #eventListeners = [];  
  isAuthenticated = false;
  #tokenRecreationTimer = null;

  constructor() {
    const accessToken = localStorage.getItem('accessToken');
    if (accessToken) {
      api.defaults.headers['Authorization'] = `Bearer ${accessToken}`;
    }      
  }

  _setTokenRecreationTimer = () => {
    clearInterval(this.#tokenRecreationTimer);
    this.#tokenRecreationTimer = setInterval(this.recreateTokens, TOKEN_RECREATION_INTERVAL_MS);
  }

  subscribeOnStatusChange = (callback) => {
    if (callback && !this.#eventListeners.includes(callback)) {
      this.#eventListeners.push(callback);
    }
  };

  login = async (email, password) => {
    const response = await api.post(API_ROUTES.login, { email, password });
    const authData = response.data;
    saveAuthData(response.data);
    api.defaults.headers['Authorization'] = `Bearer ${authData.accessToken}`;
    this.isAuthenticated = true;
    this.#eventListeners.forEach(callback => callback());
    this._setTokenRecreationTimer();
  };

  signUp = async (data) => {
    const response = await api.post(API_ROUTES.signUp, data);
    const authData = response.data;
    saveAuthData(response.data);
    api.defaults.headers['Authorization'] = `Bearer ${authData.accessToken}`;
    this.isAuthenticated = true;
    this.#eventListeners.forEach(callback => callback());
    this._setTokenRecreationTimer();
  };

  recreateTokens = async () => {
    const refreshToken = localStorage.getItem('refreshToken');
    if (refreshToken) {
      const response = await api.put(
        API_ROUTES.recreateTokens, 
        refreshToken, 
        { headers: {'Content-Type': 'application/json'} }
      );
      const authData = response.data;
      saveAuthData(response.data);
      api.defaults.headers['Authorization'] = `Bearer ${authData.accessToken}`;
      this.isAuthenticated = true;
      this.#eventListeners.forEach(callback => callback());
      this._setTokenRecreationTimer();
    }
  };

  logout = async () => {
    await api.put(API_ROUTES.logout);
    delete api.defaults.headers['Authorization'];
    this.isAuthenticated = false;
    this.#eventListeners.forEach(callback => callback());
    clearInterval(this.#tokenRecreationTimer);
    clearAuthData();
  };  
}