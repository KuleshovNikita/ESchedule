import { useStore } from "../../../api/stores/StoresManager";
import { useCult } from "../../../hooks/Translator";
import { Typography } from "@material-ui/core";
import RulesList from "../../../components/schedule/RulesList";
import ScheduleViewer from "../../../components/schedule/ScheduleViewer";
import LessonsViewer from "../../../components/lessons/LessonsViewer";
import CustomAccordion from "../../../components/CustomAccordion";
import AddUserToTenant from "../../../components/AddUserToTenant";
import Box from "@mui/material/Box";

export default function ScheduleRedactorPage() {
    const { translator } = useCult();
    const { tenantStore } = useStore();

    return (
    <Box>
        <Box sx={{ml: 1}}>
            <Typography variant="h2">
                {tenantStore.tenant?.name}
            </Typography>
            
            <CustomAccordion title={translator('labels.rules-list')}>
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
            </CustomAccordion>
        </Box>
    </Box>);
}