import { ScheduleModel } from "../../models/Schedules";
import { useEffect, useState } from "react";
import { useStore } from "../../api/stores/StoresManager";
import { normalizeUserName } from "../../utils/Utils";
import Box from "@mui/material/Box";
import Loader from "../hoc/loading/Loader";
import { useLoader } from "../../hooks/Loader";

interface Props {
    item: ScheduleModel
}

export default function ScheduleCellContent({ item }: Props) {
    const {scheduleStore} = useStore();
    const [itemState, setItemState] = useState(item);
    const loader = useLoader();

    useEffect(() => {
        const fecthItem = async () => {
            const res = await scheduleStore.getScheduleItem(item.id);
            setItemState(res);
            loader.hide();
        }

        loader.show();
        fecthItem();
    })

    const normalizeTime = () => {
        const startTime = itemState.startTime.toLocaleTimeString([], { hour12: false });
        const endTime = itemState.endTime.toLocaleTimeString([], { hour12: false });

        return `${startTime} - ${endTime}`; 
    }

    return(
        <Loader type='spin' replace>
            <Box>
                { itemState.studyGroup.title }
            </Box>
            <Box>
                { `${itemState.lesson.title} - ${normalizeUserName(itemState.teacher)}` }
            </Box>
            <Box>
                { normalizeTime() }
            </Box>
        </Loader>
    );
}