import { ScheduleModel } from "../../models/Schedules";
import { ScheduleItemPlaceholderStyle } from "../../pages/schedules/ScheduleTableStyles";
import { normalizeUserName } from "../../utils/Utils";
import Box from "@mui/material/Box";

interface Props {
    item: ScheduleModel | undefined
}

export default function ScheduleCellContent({ item }: Props) {
    const timeOptions: Intl.DateTimeFormatOptions = { 
        timeStyle: 'short', 
        hour12: false 
    }

    const normalizeTime = () => {



        const startTime = item?.startTime.toLocaleTimeString([], timeOptions);
        const endTime = item?.endTime.toLocaleTimeString([], timeOptions);

        return `${startTime} - ${endTime}`; 
    }

    return(
        <Box>
        {
            item
        &&
            <Box>
                <Box sx={{borderBottom: "1px black solid"}}>
                    <b>{ item.lessonName }</b>
                </Box>
                <Box>
                    { normalizeTime() }
                </Box>
                <Box>
                    { item.groupName }
                </Box>
                <Box>
                    { normalizeUserName(item.teacher) }
                </Box>
            </Box>
        }
        
        </Box>
    );
}