import { Route, Routes, Navigate } from "react-router";
import NotFound from "../../pages/notFound/NotFound";
import { useStore } from "../../api/stores/StoresManager";
import { Auth } from "../hoc/RequiresAuth";
import { LoginPage } from "../../pages/login/Login";
import UserPage from "../../pages/userProfile/UserProfile";
import RegistrationPage from "../../pages/registration/Registration";
import ConfirmEmailPage from "../../pages/confirmEmail/ConfirmEmail";
import { ScheduleTablePage } from "../../pages/schedules/ScheduleTable";
import { Role } from "../../models/Users";
import TenantManager from "../../pages/tenant/management/TenantManager";
import { CreateTenant } from "../../pages/tenant/creation/CreateTenant";
import LessonsManager from "../../pages/lessons/LessonsManager";
import ScheduleViewer from "../schedule/ScheduleViewer";
import AddUserToTenant from "../../pages/tenant/users/AddUserToTenant";
import RulesList from "../schedule/RulesList";
import { TenantAccessRequests } from "../../pages/tenant/users/TenantAccessRequests";
import pageRoutes from "../../utils/RoutesProvider";

export default function AppRoutes() {
    return (
        <Routes>
              <Route path="" 
                   element={ <Navigate to={pageRoutes.profile}/> } 
              />
              <Route path={pageRoutes.register}
                   element={ <RegistrationPage /> } 
              />
              <Route path={pageRoutes.login}
                   element={ <LoginPage/> } 
              />
              <Route path={pageRoutes.createTenant}
                   element={ <CreateTenant/> } 
              />
              <Route path={pageRoutes.logout}
                   element={ <Logout/> } 
              />
              <Route path={pageRoutes.confirmEmail + "/:key"} 
                   element={ <ConfirmEmailPage/> } 
              />
              <Route path={pageRoutes.profile}
                   element={ <Auth><UserPage /></Auth> } 
               />

               <Route path={pageRoutes.lessonsManagement}
                   element={ <Auth role={Role.Dispatcher}><LessonsManager /></Auth> } 
              />
              <Route path={pageRoutes.schedulesManagement}
                   element={ <Auth role={Role.Dispatcher}><ScheduleViewer /></Auth> } 
              />
              <Route path={pageRoutes.rulesManagement}
                   element={ <Auth role={Role.Dispatcher}><RulesList /></Auth> } 
              />
              <Route path={pageRoutes.manualMemberAddManagement}
                   element={ <Auth role={Role.Dispatcher}><AddUserToTenant /></Auth> } 
              />
              <Route path={pageRoutes.scheduleView + "/:scope/:targetId"} 
                   element={ <Auth><ScheduleTablePage /></Auth> } 
              />
              <Route path={pageRoutes.managementPage}
                   element={ <Auth role={Role.Dispatcher}><TenantManager /></Auth> } 
              />
              <Route path={pageRoutes.membershipRequestsManagement} 
                   element={ <Auth role={Role.Dispatcher}><TenantAccessRequests /></Auth> } 
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
        <Navigate to={pageRoutes.login} replace={true}/>
    )
}