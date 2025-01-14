import Box from "@mui/material/Box";
import { pageStyle } from "./TenantManagerStyles";
import PageBox from "../../../components/wrappers/PageBox";
import { TenantManagerButton } from "./TenantManagerButton";

export default function TenantManager() {
    return (
        <PageBox>
            <Box sx={pageStyle}>
                <TenantManagerButton 
                    icon={'edit'} 
                    text={'labels.rules-list'} 
                    route={'/rulesManagement'}                
                />

                <TenantManagerButton 
                    icon={'calendar'} 
                    text={'labels.schedule-viewer'} 
                    route={'/scheduleManagement'}                
                />

                <TenantManagerButton 
                    icon={'preview'} 
                    text={'labels.lessons-viewer'} 
                    route={'/lessonsManagement'}                
                />

                <TenantManagerButton 
                    icon={'person add'} 
                    text={'labels.add-user-to-tenant'} 
                    route={'/addUserToTenant'}                
                />

                <TenantManagerButton 
                    icon={'list'} 
                    text={'labels.view-tenant-requests'} 
                    route={'/viewTenantRequests'}                
                />
            </Box>
        </PageBox>
    );
}