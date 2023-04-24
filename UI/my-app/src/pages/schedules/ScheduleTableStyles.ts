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

export const scheduleRedactorStyle = {
    display: "grid", 
    gridTemplateColumns: "1fr 4fr"
}

export const verticalBorder = {
    borderLeft: '1px solid black'
}

export const rulesListStyle = {
    margin: "0px 10px",
}

export const ScheduleItemStyle = {
    ...ScheduleItemPlaceholderStyle,
    backgroundColor: 'rgb(255, 186, 94)',
    '&:hover': {
        backgroundColor: 'rgb(232, 169, 86)',
    },
    pb: 0,
    pt: 1,
    display: 'flex',
    flexDirection: 'column',
    '*:last-child': {
        marginTop: 'auto',
        mb: 1
    },
    fontSize: '17px'
}

export const ScheduleRowStyle = {
    ...RowAlignStyle,
    height: '130px',
    "& td": { 
        border: 0 
    }
}
