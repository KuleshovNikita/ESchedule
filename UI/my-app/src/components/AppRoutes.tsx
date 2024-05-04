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
import TenantManager from "../pages/tenant/management/TenantManager";
import { CreateTenant } from "../pages/tenant/creation/CreateTenant";
import LessonsManager from "../pages/lessons/LessonsManager";
import ScheduleViewer from "./schedule/ScheduleViewer";
import AddUserToTenant from "../pages/tenant/users/AddUserToTenant";
import RulesList from "./schedule/RulesList";

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

               <Route path="/lessonsManagement" 
                   element={ <Auth role={Role.Dispatcher}><LessonsManager /></Auth> } 
              />
              <Route path="/scheduleManagement" 
                   element={ <Auth role={Role.Dispatcher}><ScheduleViewer /></Auth> } 
              />
              <Route path="/rulesManagement" 
                   element={ <Auth role={Role.Dispatcher}><RulesList /></Auth> } 
              />
              <Route path="/addUserToTenant" 
                   element={ <Auth role={Role.Dispatcher}><AddUserToTenant /></Auth> } 
              />
              <Route path="/schedule/:scope/:targetId" 
                   element={ <Auth><ScheduleTablePage /></Auth> } 
              />
              <Route path="/tenantManager" 
                   element={ <Auth role={Role.Dispatcher}><TenantManager /></Auth> } 
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