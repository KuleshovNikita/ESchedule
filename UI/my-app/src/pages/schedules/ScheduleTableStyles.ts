import { RowAlignStyle } from "../../components/markups/timeTable/TimeTableMarkupStyles"

export const ScheduleTableHeadStyle = {
    mt: '180px',
    position: 'absolute',
}

export const ScheduleItemPlaceholderStyle = {
    borderRadius: '10px',
    height: '111px',
    ml: 1,
    mr: 1,
    pb: 4,
}

export const ScheduleItemStyle = {
    ...ScheduleItemPlaceholderStyle,
    backgroundColor: 'rgb(255, 186, 94)',
    '&:hover': {
        backgroundColor: 'rgb(232, 169, 86)',
    },
    pb: 0,
    pt: 1,
}

export const ScheduleRowStyle = {
    ...RowAlignStyle,
    height: '130px',
    "& td": { 
        border: 0 
    }
}