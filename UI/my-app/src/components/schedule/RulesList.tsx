import { useEffect, useState } from "react";
import { useStore } from "../../api/stores/StoresManager";
import { Box, MenuItem, Select } from "@mui/material";
import LoadingComponent from "../hoc/loading/LoadingComponent";
import { pascalToDashCase } from "../../utils/Utils";
import { useCult } from "../../hooks/Translator";

interface Props {
    tenantId: string
}

export default function RulesList({ tenantId }: Props) {
    const { scheduleStore } = useStore();
    const [isLoaded, setLoaded] = useState(false);
    const { translator } = useCult();

    useEffect(() => {
        const fetchRules = async () => {
            await scheduleStore.getScheduleRules(tenantId);
            setLoaded(true);
        }

        fetchRules();
    });

    return(
        <>
        {
            !isLoaded
        ?   
            <LoadingComponent type='circle'/>
        :
            scheduleStore.rules?.map((v, k) => {
                return <Box key={k} sx={{border: "1px solid black"}}>
                            <Select defaultValue={0}>
                                <MenuItem value={0}>
                                    {translator(pascalToDashCase(v.RuleName))}
                                </MenuItem>
                            </Select> - {v.Actor.Name}
                        </Box>
            })
        }
        </>
    )
}