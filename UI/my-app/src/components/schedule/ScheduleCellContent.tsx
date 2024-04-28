import { ScheduleModel } from "../../models/Schedules";
import { useEffect, useState } from "react";
import { useStore } from "../../api/stores/StoresManager";
import LoadingComponent from "../hoc/loading/LoadingComponent";
import { normalizeUserName } from "../../utils/Utils";
import Box from "@mui/material/Box";

interface Props {
    item: ScheduleModel
}

export default function ScheduleCellContent({ item }: Props) {
    const {scheduleStore} = useStore();
    const [itemState, setItemState] = useState(item);
    const [isLoaded, setLoaded] = useState(false);

    useEffect(() => {
        const fecthItem = async () => {
            const res = await scheduleStore.getScheduleItem(item.id);
            setItemState(res);
            setLoaded(true);
        }

        fecthItem();
    })

    const normalizeTime = () => {
        const startTime = itemState.startTime.toLocaleTimeString([], { hour12: false });
        const endTime = itemState.endTime.toLocaleTimeString([], { hour12: false });

        return `${startTime} - ${endTime}`; 
    }

    return(<>
        {
            !isLoaded
        ?
            <LoadingComponent type="circle"/>
        :
            <>
                <Box>
                    { itemState.studyGroup.title }
                </Box>
                <Box>
                    { `${itemState.lesson.title} - ${normalizeUserName(itemState.teacher)}` }
                </Box>
                <Box>
                    { normalizeTime() }
                </Box>
            </>
        }
    </>);
}