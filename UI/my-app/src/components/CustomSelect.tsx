import { noneWord } from "../utils/Utils";
import { useCult } from "../hooks/useTranslator";
import InputLabel from "@mui/material/InputLabel";
import MenuItem from "@mui/material/MenuItem";
import Select from "@mui/material/Select";

const style = {
    mt: 1, 
}

export interface SelectItem {
    id: string,
    value: string,
    type: string
} 

type SelectChanged = (event: any) => void

interface Props {
    label: string,
    onChange: SelectChanged,
    collection: SelectItem[] | null,
    errorHandler: any
}

export default function CustomSelect({collection, label, onChange, errorHandler}: Props) {
    const { translator } = useCult();
    
    return(<>
        <InputLabel sx={style}>{label}</InputLabel>
        <Select defaultValue={noneWord}
                onChange={onChange}
        >
            <MenuItem key={-1} value={noneWord}>{translator(noneWord)}</MenuItem>
            {
                collection?.map((v, k) => {
                    return <MenuItem key={k} value={JSON.stringify(v)}>{v.value}</MenuItem>
                })
            }
        </Select>
        { 
            errorHandler()
        }
    </>);
}