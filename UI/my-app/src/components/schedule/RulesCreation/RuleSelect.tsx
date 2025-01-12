import { InputLabel, MenuItem, Select } from "@mui/material"
import { useCult } from "../../../hooks/useTranslator";
import { availableRules } from "../../../utils/ruleUtils";
import { useState } from "react";
import RuleBodyCreator from "./RuleBodyCreator";
import { noneWord } from "../../../utils/Utils";

interface Props {
    onConfirm: any
}

export default function RuleSelect({onConfirm}: Props) {
    const { translator } = useCult();
    const [ruleName, setRuleName] = useState<string>();
    let bodyData: any = {};

    const showBody = (value: string) => {
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
                Object.values(availableRules).map((value, k) => {
                    return <MenuItem key={k} value={value}>
                                {translator(value)}
                            </MenuItem>
                })
            }
        </Select>

        { ruleName && <RuleBodyCreator ruleName={ruleName} 
                                       bodyData={bodyData}
                                       onConfirm={onConfirm} /> }
    </>);
}