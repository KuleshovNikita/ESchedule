import { Route, Routes, Navigate } from "react-router";
import NotFound from "../pages/notFound/NotFound";
import { useStore } from "../api/stores/StoresManager";
import { RequireAuth } from "./hoc/RequiresAuth";
import { Login } from "../pages/login/Login";
import UserProfile from "../pages/userProfile/UserProfile";
import Registration from "../pages/registration/Registration";

export default function AppRoutes() {
    return (
        <Routes>
            <Route path="" element={ <Navigate to="/profile"/> } />

            <Route path="/register" element={ <Registration /> } />
            <Route path="/login" element={ <Login/> } />
            <Route path="/logout" element={ <Logout/> } />

            <Route path="/profile" element={ <RequireAuth><UserProfile /></RequireAuth> } />

            <Route path="*" element={ <NotFound /> } />
        </Routes>
    );
}

function Logout() {
    const { userStore } = useStore();

    userStore.logout();

    return (
        <Navigate to="/login" replace={true}/>
    )
}