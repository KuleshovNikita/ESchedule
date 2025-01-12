import { ReactNode } from "react";
import { useStore } from "../api/stores/StoresManager"

export const useTenantCheck = () => {
    const { tenantStore } = useStore();

    return (componentFunc: () => ReactNode) => {
        return (
            tenantStore.tenant != null
         &&
            componentFunc()
        )
    }
} 