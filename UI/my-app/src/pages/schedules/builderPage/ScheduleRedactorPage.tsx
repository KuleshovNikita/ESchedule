import { useEffect, useState } from "react";
import { useStore } from "../../../api/stores/StoresManager";
import { useCult } from "../../../hooks/Translator";
import { Typography } from "@material-ui/core";
import LoadingComponent from "../../../components/hoc/loading/LoadingComponent";
import RulesList from "../../../components/schedule/RulesList";
import { Box } from "@mui/material";
import ScheduleViewer from "../../../components/schedule/ScheduleViewer";

export default function ScheduleRedactorPage() {
    const { translator } = useCult();
    const { tenantStore, userStore } = useStore();
    const [isLoaded, setLoaded] = useState(false);

    useEffect(() => {
        const fetchTenant = async () => {
            await tenantStore.getTenant(userStore.user?.tenantId as string);
            setLoaded(true);
        }

        fetchTenant();
    }, [tenantStore, userStore.user?.tenantId])

    return (
    <Box>
        {
            !isLoaded
        ?   
            <LoadingComponent/>
        :   
            <Box sx={{ml: 1}}>
                <Typography variant="h2">
                    {tenantStore.tenant?.tenantName}
                </Typography>
                
                <hr/>
                <Box>
                    <Typography variant="h4">
                        {translator('label.rules-list')}
                    </Typography>
                    <RulesList tenantId={tenantStore.tenant?.id as string}/>
                </Box>

                <hr/>
                <Box>
                    <Typography variant="h4">
                        {translator('label.schedule-viewer')}
                    </Typography>
                    <ScheduleViewer/>
                </Box>
            </Box>
        }
    </Box>);
}