import { TextField } from "@mui/material";
import { RuleModel } from "../../models/Schedules";
import { TeacherBusyDayRule } from "../../models/Rules";
import { daysOfWeek } from "../../utils/Utils";
import { useStore } from "../../api/stores/StoresManager";
import { useState } from "react";
import { UserModel } from "../../models/Users";
import { useCult } from "../../hooks/Translator";

interface Props {
    rule: RuleModel
}

const style = {
    ml: 1, 
    width: "600px"
}

export default function RuleBodyViewer({rule}: Props) {
    const { translator } = useCult();
    const { userStore } = useStore();
    const [teacherInfo, setTeacherInfo] = useState<UserModel>();

    switch(rule.ruleName) {
        case "TeacherBusyDayRule": {
            const result = JSON.parse(rule.ruleJson) as TeacherBusyDayRule;

            if(!teacherInfo) {
                userStore.getUserInfo(result.ActorId).then(res => setTeacherInfo(res));
            }            

            return <TextField 
                        sx={style} 
                        value={`Teacher ${teacherInfo?.name} is busy on ${daysOfWeek[result.Target]}`} 
                    />;
        }
        
        default: 
            return (<>
                {translator("message.no-rule-found")}
            </>);
    }
}