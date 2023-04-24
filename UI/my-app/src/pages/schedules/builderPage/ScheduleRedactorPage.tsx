import { useEffect, useState } from "react";
import { useStore } from "../../../api/stores/StoresManager";
import { useCult } from "../../../hooks/Translator";
import { Typography } from "@material-ui/core";
import LoadingComponent from "../../../components/hoc/loading/LoadingComponent";
import RulesList from "../../../components/schedule/RulesList";
import { buttonHoverStyles, buttonImageIconStyle } from "../../../styles/ButtonStyles";
import { Box, Button } from "@mui/material";
import AddCircleIcon from '@mui/icons-material/AddCircle';
import SaveIcon from '@mui/icons-material/Save';
import { rulesListButtonsStyle } from "../ScheduleTableStyles";

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
                    <Box sx={rulesListButtonsStyle}>
                        <Button 
                            sx={buttonHoverStyles} 
                            variant="contained"
                        >
                            {translator('buttons.add-new-rule')}
                            <AddCircleIcon sx={buttonImageIconStyle} />
                        </Button>
                        <Button
                            sx={buttonHoverStyles}   
                            variant="contained"         
                        >
                            {translator('buttons.save')}
                            <SaveIcon sx={buttonImageIconStyle}/>
                        </Button>
                    </Box>
                </Box>
            </Box>
        }
    </Box>);
}