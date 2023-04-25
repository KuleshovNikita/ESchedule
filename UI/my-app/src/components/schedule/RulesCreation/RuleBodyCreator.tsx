import { useState } from "react";
import { useCult } from "../../../hooks/Translator";
import CreateTeacherBudyDayRuleComp from "./CreateTeacherBudyDayRuleComp";

interface Props {
    ruleName: string
}

export default function RuleBodyCreator({ruleName}: Props) {
    const { translator } = useCult();
    const [hasErrors, setHasErrors] = useState(false);

    switch(ruleName) {
        case "words.none": {
            return <></>;
        }

        case "rule.teacher-busy-day": {
            return <CreateTeacherBudyDayRuleComp setHasErrors={setHasErrors}></CreateTeacherBudyDayRuleComp>
        }
        
        default: 
            return (<>
                {translator("message.no-rule-found")}
            </>);
    }
}