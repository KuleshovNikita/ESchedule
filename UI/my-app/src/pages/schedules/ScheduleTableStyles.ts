import { RowAlignStyle } from "../../components/markups/timeTable/TimeTableMarkupStyles"

export const ScheduleTableHeadStyle = {
    mt: '180px',
    position: 'absolute',
    width: '96%'
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

export const newRuleFormStyle = {
    display: 'flex',
    alignItems: 'end',
    flexDirection: 'column',
    margin: '5% auto',
    width: "600px",
    backgroundColor: 'white',
    borderRadius: '15px',
    padding: '10px',
    pb: 2
}

export const ruleFormCloseButtonStyle = {
    backgroundColor: 'rgb(25, 118, 210, 1)',
    padding: '5px',
    color: 'white',
    '&:hover': {
        backgroundColor: 'orange',
    }
}

export const ruleBodyFormStyle = {
    display: 'flex', 
    flexDirection: 'column',
    width: '100%'
}

export const outerTransperancyStyle = {
    position: 'fixed',
    zIndex: 1,
    left: 0,
    top: 0,
    backgroundColor: 'rgb(0, 0, 0, 0.8)',
    width: '100%',
    height: '100%'
}

export const ScheduleItemPlaceholderStyle = {
    borderRadius: '10px',
    height: '111px',
    ml: 1,
    mr: 1,
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

export const ScheduleItemStyle = {
    ...ScheduleItemPlaceholderStyle,
    backgroundColor: 'rgb(255, 186, 94)',
    '&:hover': {
        backgroundColor: 'rgb(232, 169, 86)',
    }
}

export const rulesListButtonsStyle = {
    display: 'flex', 
    flexDirection: 'column',
    width: "300px"
}

export const ScheduleRowStyle = {
    ...RowAlignStyle,
    height: '130px',
    "& td": { 
        border: 0 
    }
}
