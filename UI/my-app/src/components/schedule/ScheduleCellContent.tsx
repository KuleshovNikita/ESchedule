import { ScheduleModel } from "../../models/Schedules";
import { normalizeUserName } from "../../utils/Utils";
import Box from "@mui/material/Box";

interface Props {
    item: ScheduleModel
}

export default function ScheduleCellContent({ item }: Props) {
    const timeOptions: Intl.DateTimeFormatOptions = { 
        timeStyle: 'short', 
        hour12: false 
    }

    const normalizeTime = () => {
        const startTime = item.startTime.toLocaleTimeString([], timeOptions);
        const endTime = item.endTime.toLocaleTimeString([], timeOptions);

        return `${startTime} - ${endTime}`; 
    }

    return(
        <Box>
            <Box sx={{backgroundColor: "blue"}}>
                { normalizeTime() }
            </Box>
            <Box>
                { item.lessonName }
            </Box>
            <Box>
                { item.groupName }
            </Box>
            <Box>
                { normalizeUserName(item.teacher) }
            </Box>
        </Box>
    );
}