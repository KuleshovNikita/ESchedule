import {Navigate, useLocation} from "react-router-dom";
import { useStore } from "../../api/stores/StoresManager";
import { Role } from "../../models/Users";
import { toast } from "react-toastify";

type Props = {
    children: JSX.Element,
    role?: Role | null
}

export const Auth = ({children, role = null}: Props) => {
    const location = useLocation();
    const { userStore } = useStore();

    if (!userStore.isLoggedIn) {
        return <Navigate to='/login' state={{from: location}}/>
    }

    if(role) {
        if(userStore.user?.role === role) {
            return children;
        } else {
            toast.error('You do not have permission to access this page');
            return <Navigate to='/profile' />
        }
    }

    return children;
}
