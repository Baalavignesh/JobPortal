import { Counter } from "./components/Counter";
import { FetchData } from "./components/FetchData";
import { Home } from "./components/Home";
import AuthPage from "./pages/AuthPage/AuthPage";

const AppRoutes = [
  {
    index: true,
    element: <AuthPage />
  },
  {
    path: '/counter',
    element: <Counter />
  },
  {
    path: '/fetch-data',
    element: <FetchData />
  }
];

export default AppRoutes;
