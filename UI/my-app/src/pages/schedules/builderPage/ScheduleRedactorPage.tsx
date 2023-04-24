import { useEffect, useState } from "react";
import { useStore } from "../../../api/stores/StoresManager";
import { useCult } from "../../../hooks/Translator";
import { Typography } from "@material-ui/core";
import LoadingComponent from "../../../components/hoc/loading/LoadingComponent";
import RulesList from "../../../components/schedule/RulesList";
import { buttonAddRuleStyle, buttonHoverStyles } from "../../../styles/ButtonStyles";
import { Box, Button } from "@mui/material";
import AddCircleIcon from '@mui/icons-material/AddCircle';

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
                    <Button 
                        sx={buttonHoverStyles} 
                        variant="contained"
                    >
                        {translator('buttons.add-new-rule')}
                        <AddCircleIcon sx={buttonAddRuleStyle} />
                    </Button>
                </Box>
            </Box>
        }
    </Box>);
}