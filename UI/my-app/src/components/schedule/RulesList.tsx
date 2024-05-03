import { useEffect, useState } from "react";
import { useStore } from "../../api/stores/StoresManager";
import Loader from "../hoc/loading/Loader";
import { useCult } from "../../hooks/Translator";
import RuleBodyViewer from "./RuleBodyViewer";
import { buttonHoverStyles, buttonImageIconStyle } from "../../styles/ButtonStyles";
import { rulesListButtonsStyle } from "../../pages/schedules/ScheduleTableStyles";
import PopupForm from "../modalWindow/PopupForm";
import { toast } from "react-toastify";
import RuleSelect from "./RulesCreation/RuleSelect";
import { RuleModel } from "../../models/Schedules";
import Box from "@mui/material/Box";
import TextField from "@mui/material/TextField";
import Button from "@mui/material/Button";
import { useLoader } from "../../hooks/Loader";
import Icon from "../wrappers/Icon";

export default function RulesList() {
    const { scheduleStore } = useStore();
    const [rules, setRules] = useState<RuleModel[]>([]);
    const [isCreatingNewRule, setCreatingNewRuleFlag] = useState(false);
    const { translator } = useCult();
    const loader = useLoader();

    useEffect(() => {
        const fetchRules = async () => 
            await scheduleStore.getScheduleRules()
                .then(res => setRules(res))
                .then(() => loader.hide());

        loader.show();
        fetchRules();
    }, [scheduleStore]);

    const showNewRuleForm = () => {
        setCreatingNewRuleFlag(!isCreatingNewRule);
    }

    const createSchedule = async () => 
        await scheduleStore.buildSchedule()
            .then(() => toast.success(translator('toasts.schedule-created-successfully')));

    const removeRule = async (ruleId: string) => {
        await scheduleStore.removeRule(ruleId)
            .then(() => setRules(rules.filter(x => x.id !== ruleId)));
       
    }

    return(
        <Loader type='spin' replace>
            {
                rules?.map((v, k) => {
                    return <Box key={k} sx={{mt: 1}}>
                                <TextField value={translator(v.ruleName)}/>
                                <RuleBodyViewer rule={v}/>
                                <Button sx={{...buttonHoverStyles, ml: 3}} 
                                        variant="contained"
                                        onClick={() => removeRule(v.id)}
                                >
                                    {translator("buttons.remove")}
                                </Button>
                            </Box>
                })
            }

            <Box sx={rulesListButtonsStyle}>
                <Button 
                    sx={buttonHoverStyles} 
                    variant="contained"
                    onClick={showNewRuleForm}
                >
                    {translator('buttons.create-new-rule')}
                    <Icon type='add'/>
                </Button>
                <Button
                    sx={buttonHoverStyles}   
                    variant="contained"
                    onClick={createSchedule}         
                >
                    {translator('buttons.build-schedule')}
                    <Icon type='build'/>
                </Button>
            </Box>

            { 
                isCreatingNewRule
            &&
                <PopupForm closeButtonHandler={showNewRuleForm}>
                    <RuleSelect onConfirm={showNewRuleForm}/>
                </PopupForm>
            }
        </Loader>
    )
}