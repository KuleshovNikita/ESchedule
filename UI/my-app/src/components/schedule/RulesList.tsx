import { useEffect, useState } from "react";
import { useStore } from "../../api/stores/StoresManager";
import { Box, Button, TextField } from "@mui/material";
import LoadingComponent from "../hoc/loading/LoadingComponent";
import { useCult } from "../../hooks/Translator";
import RuleBodyViewer from "./RuleBodyViewer";
import AddCircleIcon from '@mui/icons-material/AddCircle';
import ConstructionIcon from '@mui/icons-material/Construction';
import { buttonHoverStyles, buttonImageIconStyle } from "../../styles/ButtonStyles";
import { rulesListButtonsStyle } from "../../pages/schedules/ScheduleTableStyles";
import CreateRuleForm from "./RulesCreation/CreateRuleForm";

interface Props {
    tenantId: string
}

export default function RulesList({ tenantId }: Props) {
    const { scheduleStore } = useStore();
    const [isLoaded, setLoaded] = useState(false);
    const [isCreatingNewRule, setCreatingNewRuleFlag] = useState(false);
    const { translator } = useCult();

    useEffect(() => {
        const fetchRules = async () => {
            await scheduleStore.getScheduleRules(tenantId);
            setLoaded(true);
        }

        fetchRules();
    }, [scheduleStore, scheduleStore.rules, tenantId]);

    const showNewRuleForm = () => {
        setCreatingNewRuleFlag(!isCreatingNewRule);
    }

    return(
        <>
        {
            !isLoaded
        ?   
            <LoadingComponent type='circle'/>
        :
            <>
                {
                    scheduleStore.rules?.map((v, k) => {
                        return <Box key={k} sx={{mt: 1}}>
                                    <TextField value={translator(v.ruleName)}/>
                                    <RuleBodyViewer rule={v}/>
                                </Box>
                    })
                }

                <Box sx={rulesListButtonsStyle}>
                    <Button 
                        sx={buttonHoverStyles} 
                        variant="contained"
                        onClick={showNewRuleForm}
                    >
                        {translator('buttons.create-new-schedule')}
                        <AddCircleIcon sx={buttonImageIconStyle} />
                    </Button>
                    <Button
                        sx={buttonHoverStyles}   
                        variant="contained"         
                    >
                        {translator('buttons.build-schedule')}
                        <ConstructionIcon sx={buttonImageIconStyle}/>
                    </Button>
                </Box>

                { 
                    isCreatingNewRule
                &&
                    <CreateRuleForm closeAction={showNewRuleForm}/>
                }
            </>
        }
        </>
    )
}