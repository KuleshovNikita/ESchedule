import { useEffect, useState } from "react";
import { useStore } from "../../api/stores/StoresManager";
import { Box } from "@mui/material";
import LoadingComponent from "../hoc/loading/LoadingComponent";

interface Props {
    tenantId: string
}

export default function RulesList({ tenantId }: Props) {
    const { scheduleStore } = useStore();
    const [isLoaded, setLoaded] = useState(false);

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
                            {v.ruleName} - {v.ruleJson}
                        </Box>
            })
        }
        </>
    )
}