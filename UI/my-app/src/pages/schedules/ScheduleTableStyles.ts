import { RowAlignStyle } from "../../components/markups/timeTable/TimeTableMarkupStyles"

export const ScheduleTableHeadStyle = {
    mt: '80px',
    position: 'absolute',
}

export const ScheduleItemPlaceholderStyle = {
    borderRadius: '10px',
    width: '154px',
    height: '40px',
    ml: 1,
    mr: 1,
    pb: 4,
}

export const ScheduleActiveItemStyle = {
    ...ScheduleItemPlaceholderStyle,
    backgroundColor: 'rgb(255, 186, 94)',
    '&:hover': {
        backgroundColor: 'rgb(232, 169, 86)',
    }
}

export const ScheduleRowStyle = {
    ...RowAlignStyle, 
    height: '88px',
    "& td": { 
        border: 0 
    }
}
