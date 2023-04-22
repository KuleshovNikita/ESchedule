import { useEffect, useState } from "react";
import { useStore } from "../../../api/stores/StoresManager";
import { useCult } from "../../../hooks/Translator";
import { Box, Typography } from "@material-ui/core";
import LoadingComponent from "../../../components/hoc/loading/LoadingComponent";
import RulesList from "../../../components/schedule/RulesList";
import TimeTableMarkup from "../../../components/markups/timeTable/TimeTableMarkup";

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
            <Box sx={{display: "grid", gridTemplateColumns: "1fr 4fr"}}>
                <Box>
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
                </Box>
                <Box>
                    <TimeTableMarkup/>
                </Box>
            </Box>
        }
    </Box>);
}