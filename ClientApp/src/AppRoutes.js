import HomePage from "./pages/Home/HomePage";
import LoginPage from "./pages/Login/LoginPage";
import SignupPage from "./pages/SignUp/SignupPage";

const AppRoutes = [
  {
    index: '/',
    element: <SignupPage />
  },

  {
    path: '/login',
    element: <LoginPage />
  },
  {
    path: '/app',
    element: <HomePage />
  }
  
];

export default AppRoutes;
