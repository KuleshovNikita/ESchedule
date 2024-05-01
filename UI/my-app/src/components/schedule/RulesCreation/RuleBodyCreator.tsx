import { useCult } from "../../../hooks/Translator";
import { buttonHoverStyles, buttonImageIconStyle } from "../../../styles/ButtonStyles";
import { useState } from "react";
import { useStore } from "../../../api/stores/StoresManager";
import { RuleInputModel } from "../../../models/Schedules";
import { availableRules } from "../../../utils/ruleUtils";
import { noneWord } from "../../../utils/Utils";
import { toast } from "react-toastify";
import Button from "@mui/material/Button";
import CreateTeacherBodyDayRuleComp from "./CreateTeacherBodyDayRuleComp";
import Icon from "../../wrappers/Icon";

interface Props {
    ruleName: string,
    bodyData: any,
    onConfirm: any
}

export default function RuleBodyCreator({ruleName, bodyData, onConfirm}: Props) {
    const { translator } = useCult();
    const [hasErrors, setHasErrors] = useState(true);
    const { scheduleStore, userStore } = useStore();

    const addSaveButton = () => {
        return (
            <Button sx={buttonHoverStyles}   
                    variant="contained"
                    disabled={hasErrors}
                    onClick={saveRule}   
            >
                {translator('buttons.create')}
                <Icon type='add'/>
            </Button>
        );
    }

    const saveRule = async () => {
        const newRule: RuleInputModel = {
            ruleName: ruleName,
            ruleJson: JSON.stringify(bodyData)
        }

        await scheduleStore.createRule(newRule)
            .then(() => toast.success(translator('toasts.rules-added-successfully')));

        onConfirm();
    }

    switch(ruleName) {
        case noneWord: {
            return <></>;
        }

        case availableRules.teacherBusyDayRule: {
            return (
                <>
                    <CreateTeacherBodyDayRuleComp setHasErrors={setHasErrors}
                                                  bodyData={bodyData} 
                    /> 
                    {addSaveButton()}
                </>
            );
        }
        
        default: 
            return (<>
                {translator("message.no-rule-found")}
            </>);
    }
}