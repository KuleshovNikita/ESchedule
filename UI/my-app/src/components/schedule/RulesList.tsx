import { useEffect, useState } from "react";
import { useStore } from "../../api/stores/StoresManager";
import { Box, Button, InputLabel, Select, TextField } from "@mui/material";
import LoadingComponent from "../hoc/loading/LoadingComponent";
import { availableRules, pascalToDashCase } from "../../utils/Utils";
import { useCult } from "../../hooks/Translator";
import RuleBody from "./RuleBody";
import AddCircleIcon from '@mui/icons-material/AddCircle';
import SaveIcon from '@mui/icons-material/Save';
import CloseIcon from '@mui/icons-material/Close';
import { buttonHoverStyles, buttonImageIconStyle } from "../../styles/ButtonStyles";
import { createRuleFormMainBoxStyle, newRuleFormStyle, outerTransperancyStyle, ruleFormCloseButtonStyle, rulesListButtonsStyle } from "../../pages/schedules/ScheduleTableStyles";
import { MenuItem } from "@material-ui/core";

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
    }, [scheduleStore.rules]);

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
                        return <Box key={k}>
                                    <TextField value={translator(pascalToDashCase(v.ruleName))}/>
                                    <RuleBody rule={v}/>
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
                        {translator('buttons.save')}
                        <SaveIcon sx={buttonImageIconStyle}/>
                    </Button>
                </Box>

                { 
                    isCreatingNewRule
                &&
                    <Box sx={outerTransperancyStyle}>
                        <Box sx={[newRuleFormStyle, createRuleFormMainBoxStyle]}>
                            <Box>
                                <Button sx={ruleFormCloseButtonStyle} onClick={showNewRuleForm} >
                                    <CloseIcon />
                                </Button>
                            </Box>
                            <Box>
                                <InputLabel>{translator('labels.rule')}</InputLabel>
                                <Select sx={{width: "300px"}}>
                                    {availableRules.map((value, key) => {
                                        return <MenuItem key={key}>{value}</MenuItem>
                                    })}
                                </Select>
                            </Box>
                        </Box>
                    </Box>
                }
            </>
        }
        </>
    )
}