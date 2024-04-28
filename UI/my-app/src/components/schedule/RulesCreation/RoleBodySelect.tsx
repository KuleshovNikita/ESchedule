import { noneWord } from "../../../utils/Utils";
import { useCult } from "../../../hooks/Translator";

interface Props {
    onChange: any,
    label: string,
    collection: [],
    children: any
}

const style = {
    mt: 1, 
}

export default function RoleBodySelect({onChange, label, collection, children}: Props) {
    const {translator} = useCult();

    return (<>
        {/* <InputLabel sx={style}>{translator('labels.day-of-week')}</InputLabel>
        <Select defaultValue={noneWord}
                onChange={e => setDay(e.target.value)}
        >
            <MenuItem key={-1} value={noneWord}>{translator(noneWord)}</MenuItem>
            {collection?.map((v, k) => {
                return <MenuItem key={k} value={k}>{translator(`days-of-week.${v}`)}</MenuItem>
            })}
        </Select> */}
    </>);
}