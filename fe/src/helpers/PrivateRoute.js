import { Navigate } from "react-router";

export default function PrivateRoute ({ children, isAuthenticated }) {
    return isAuthenticated ? children : <Navigate to="/403" replace />;
};