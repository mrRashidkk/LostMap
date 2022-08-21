import { Home } from "./components/Home";
import Map from "./components/map";
import Account from "./components/account";

const PATHS = {
  Home: '/',
  Account: '/account',
  Map: '/map'
};

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },  
  {
    path: PATHS.Map,
    element: <Map />
  },
  {
    path: PATHS.Account,
    element: <Account />
  }
];

export { PATHS };

export default AppRoutes;
