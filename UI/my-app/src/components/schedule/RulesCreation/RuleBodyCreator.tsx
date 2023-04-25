import { useCult } from "../../../hooks/Translator";
import CreateTeacherBodyDayRuleComp from "./CreateTeacherBudyDayRuleComp";
import { Button } from "@mui/material";
import SaveIcon from '@mui/icons-material/Save';
import { buttonHoverStyles, buttonImageIconStyle } from "../../../styles/ButtonStyles";
import { useState } from "react";

interface Props {
    ruleName: string,
    bodyData: any
}

export default function RuleBodyCreator({ruleName, bodyData}: Props) {
    const { translator } = useCult();
    const [hasErrors, setHasErrors] = useState(true);

    const addSaveButton = () => {
        return (
            <Button sx={buttonHoverStyles}   
                    variant="contained"
                    disabled={hasErrors}
                    onClick={saveRule}   
            >
                {translator('buttons.save')}
                <SaveIcon sx={buttonImageIconStyle}/>
            </Button>
        );
    }

    const saveRule = () => {
        bodyData.ruleName = ruleName;
        bodyData = JSON.stringify(bodyData); 
        console.log(bodyData);
    }

    switch(ruleName) {
        case "words.none": {
            return <></>;
        }

        case "rule.teacher-busy-day": {
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