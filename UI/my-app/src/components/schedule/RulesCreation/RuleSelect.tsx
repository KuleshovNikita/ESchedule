import { InputLabel, MenuItem, Select } from "@mui/material"
import { useCult } from "../../../hooks/Translator";
import { availableRules } from "../../../utils/ruleUtils";
import { useState } from "react";
import RuleBodyCreator from "./RuleBodyCreator";
import { noneWord } from "../../../utils/Utils";

interface Props {
    switchSaveButton: any
}

export default function RuleSelect({switchSaveButton}: Props) {
    const { translator } = useCult();
    const [ruleName, setRuleName] = useState<string>();

    const showBody = (value: string) => {
        switchSaveButton(value === noneWord);
        setRuleName(value);
    }

    return(
    <>
        <InputLabel>{translator('labels.rule')}</InputLabel>
        <Select 
            onChange={(e) => showBody(e.target.value as string)} 
            sx={{mb: 1}}
            defaultValue={noneWord}
        >
            <MenuItem key={-1} value={noneWord}>{translator(noneWord)}</MenuItem>
            {
                availableRules.map((value, k) => {
                    return <MenuItem key={k} value={value}>
                                {translator(value)}
                            </MenuItem>
                })
            }
        </Select>

        {
            ruleName
        &&
            <RuleBodyCreator ruleName={ruleName} />
        }
    </>);
}