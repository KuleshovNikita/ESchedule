import { useStore } from "../../../api/stores/StoresManager";
import { useCult } from "../../../hooks/Translator";
import { Typography } from "@material-ui/core";
import RulesList from "../../../components/schedule/RulesList";
import ScheduleViewer from "../../../components/schedule/ScheduleViewer";
import LessonsViewer from "../../../components/lessons/LessonsViewer";
import CustomAccordion from "../../../components/CustomAccordion";
import AddUserToTenant from "../../../components/AddUserToTenant";
import Box from "@mui/material/Box";
import { Button } from "@mui/material";
import Icon from "../../../components/wrappers/Icon";
import { btnStyle, iconStyle, pageStyle } from "./TenantManagementStyles";

export default function TenantManagementPage() {
    const { translator } = useCult();
    const { tenantStore } = useStore();

    return (
    <Box>
        <Box sx={{ml: 1}}>
            <Typography variant="h2">
                {tenantStore.tenant?.name}
            </Typography>

            <Box sx={pageStyle}>
                <Button sx={btnStyle}>
                    <Icon type='edit' sx={iconStyle}/>
                    <Typography>
                        {translator('labels.rules-list')}
                    </Typography>
                </Button>

                <Button sx={btnStyle}>
                    <Icon type='calendar' sx={iconStyle}/>
                    <Typography>
                        {translator('labels.schedule-viewer')}
                    </Typography>
                </Button>

                <Button sx={btnStyle}>
                    <Icon type='preview' sx={iconStyle}/>
                    <Typography>
                        {translator('labels.lessons-viewer')}
                    </Typography>
                </Button>

                <Button sx={btnStyle}>
                    <Icon type='person add' sx={iconStyle}/>
                    <Box>
                        <Typography>
                            {translator('labels.add-user-to-tenant')}
                        </Typography>
                    </Box>
                </Button>
            </Box>
            
            {/* <CustomAccordion title={translator('labels.rules-list')}>
                <RulesList tenantId={tenantStore.tenant?.id as string}/>
            </CustomAccordion>
            <hr/>

            <CustomAccordion title={translator('labels.schedule-viewer')}>
                <ScheduleViewer/>
            </CustomAccordion>
            <hr/>

            <CustomAccordion title={translator('labels.lessons-viewer')}>
                <LessonsViewer/>
            </CustomAccordion>
            <hr/>

            <CustomAccordion title={translator('labels.add-user-to-tenant')}>
                <AddUserToTenant/>
            </CustomAccordion> */}
        </Box>
    </Box>);
}