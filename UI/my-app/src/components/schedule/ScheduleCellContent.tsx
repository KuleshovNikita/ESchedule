import { Box } from "@mui/material";
import { ScheduleModel } from "../../models/Schedules";

interface Props {
    item: ScheduleModel
}

export default function ScheduleCellContent({ item }: Props) {

    return(
        <>
            <Box>
                { item.studyGroup.title }
            </Box>
            <Box>
                { item.lesson.title }
            </Box>
            <Box>
                { item.startTime.toLocaleTimeString([], { hour12: false }) } - { item.endTime.toLocaleTimeString([], { hour12: false }) }
            </Box>
        </>
    );
}