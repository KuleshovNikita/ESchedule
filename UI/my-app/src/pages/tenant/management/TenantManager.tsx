import Box from "@mui/material/Box";
import { pageStyle } from "./TenantManagerStyles";
import PageBox from "../../../components/wrappers/PageBox";
import { TenantManagerButton } from "./TenantManagerButton";
import pageRoutes from "../../../utils/RoutesProvider";

export default function TenantManager() {
    return (
        <PageBox>
            <Box sx={pageStyle}>
                <TenantManagerButton 
                    icon={'edit'} 
                    text={'labels.rules-list'} 
                    route={pageRoutes.rulesManagement}                
                />

                <TenantManagerButton 
                    icon={'calendar'} 
                    text={'labels.schedule-viewer'} 
                    route={pageRoutes.schedulesManagement}                
                />

                <TenantManagerButton 
                    icon={'preview'} 
                    text={'labels.lessons-viewer'} 
                    route={pageRoutes.lessonsManagement}                
                />

                <TenantManagerButton 
                    icon={'person add'} 
                    text={'labels.add-user-to-tenant'} 
                    route={pageRoutes.manualMemberAddManagement}                
                />

                <TenantManagerButton 
                    icon={'list'} 
                    text={'labels.view-tenant-requests'} 
                    route={pageRoutes.membershipRequestsManagement}                
                />
            </Box>
        </PageBox>
    );
}