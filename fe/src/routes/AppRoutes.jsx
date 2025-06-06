import { createBrowserRouter, RouterProvider } from "react-router-dom";
import NotFoundPage from "../pages/error/NotFoundPage";
import ForbiddenPage from "../pages/error/ForbiddenPage";
import GuestHomePage from "../pages/guest/GuestHomePage";
import LoginPage from "../pages/auth/LoginPage";
import RegisterPage from "../pages/auth/RegisterPage";
import OTPVerificationPage from "../pages/auth/OTPVerificationPage";
import BloodInfoPage from "../pages/guest/BloodInfoPage";
import BlogPage from "../pages/guest/BlogPage";
import BloodDonationGuide from "../pages/guest/DonationGuide";
import ManagerHomePage from "../pages/manager/ManagerHomePage";
import MemberHomePage from "../pages/member/MemberHomePage";
import { useAuth } from "../context/AuthContext";

const AppRoutes = () => {
  const { user } = useAuth();
  // Tạo router với prop isMember truyền vào GuestHomePage
  const router = createBrowserRouter([
    {
      path: "/",
      element: <GuestHomePage isMember={user?.RoleName === "Member"} />, // truyền prop isMember dựa vào session user
    },
    {
      path: "/login",
      element: <LoginPage />,
    },
    {
      path: "/register",
      element: <RegisterPage />,
    },
    {
      path: "/:authType/otpverification",
      element: <OTPVerificationPage />,
    },
    {
      path: "/403",
      element: <ForbiddenPage />,
    },
    {
      path: "/404",
      element: <NotFoundPage />,
    },
    {
      path: "/blood-info",
      element: <BloodInfoPage />,
    },
    {
      path: "/blog",
      element: <BlogPage />,
    },
    {
      path: "/donation-guide",
      element: <BloodDonationGuide />,
    },
    {
      path: "/manager",
      element: <ManagerHomePage />,
    },
    {
      path: "/member",
      element: <MemberHomePage />,
    },
    {
      path: "*",
      element: <NotFoundPage />,
    },
  ]);
  return <RouterProvider router={router} />;
};

export default AppRoutes;
