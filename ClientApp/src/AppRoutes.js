import AdminPage from "./pages/Admin/AdminPage";
import HomePage from "./pages/Home/HomePage";
import LoginPage from "./pages/Login/LoginPage";
import PostJobPage from "./pages/PostJob/PostJobPage";
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
  },
  {
    path: '/adminpage',
    element: <AdminPage />
  },
  {
    path: '/postjob',
    element: <PostJobPage />
  }
  
];

export default AppRoutes;
