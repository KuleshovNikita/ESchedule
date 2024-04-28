import { Route, Routes, Navigate } from "react-router";
import NotFound from "../pages/notFound/NotFound";
import { useStore } from "../api/stores/StoresManager";
import { Auth } from "./hoc/RequiresAuth";
import { LoginPage } from "../pages/login/Login";
import UserPage from "../pages/userProfile/UserProfile";
import RegistrationPage from "../pages/registration/Registration";
import ConfirmEmailPage from "../pages/confirmEmail/ConfirmEmail";
import { ScheduleTablePage } from "../pages/schedules/ScheduleTable";
import { Role } from "../models/Users";
import ScheduleRedactorPage from "../pages/schedules/builderPage/ScheduleRedactorPage";
import { CreateTenant } from "../pages/tenant/CreateTenant";

export default function AppRoutes() {
    return (
        <Routes>
              <Route path="" 
                   element={ <Navigate to="/profile"/> } 
              />
              <Route path="/register" 
                   element={ <RegistrationPage /> } 
              />
              <Route path="/login" 
                   element={ <LoginPage/> } 
              />
              <Route path="/createTenant" 
                   element={ <CreateTenant/> } 
              />
              <Route path="/logout" 
                   element={ <Logout/> } 
              />
              <Route path="/confirmEmail/:key" 
                   element={ <ConfirmEmailPage/> } 
              />
              <Route path="/profile" 
                   element={ <Auth><UserPage /></Auth> } 
              />
              <Route path="/schedule/:isTeacherScope/:targetId" 
                   element={ <Auth><ScheduleTablePage /></Auth> } 
              />
              <Route path="/scheduleBuilder" 
                   element={ <Auth role={Role.Dispatcher}><ScheduleRedactorPage /></Auth> } 
              />

              <Route path="*" 
                   element={ <NotFound /> } 
              />
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