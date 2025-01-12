import { ReactNode } from "react";
import { useStore } from "../api/stores/StoresManager";
import { Role } from "../models/Users";

export const useRoleCheck = () => {
    const { userStore } = useStore();

    return (requiredRole: Role, next: () => ReactNode) => {
        return (
            userStore.user?.role === requiredRole
        &&
            next()
        );
    }
}