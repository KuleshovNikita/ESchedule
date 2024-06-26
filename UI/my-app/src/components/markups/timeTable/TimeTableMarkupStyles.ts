export const RowAlignStyle = {
    display: "grid",
    gridTemplateColumns: "repeat(7, 1fr)" ,
    ml: 8,
    mt: 1,
}

export const TimeTableHeadStyles = {
    ...RowAlignStyle,
    '*': {
        borderRight: 'solid gray 1px',
        padding: '5px',
        pt: '15px',
        pb: '15px',
        mr: 0.1,
        ml: 0.1
    },
    '&:first-of-type': {
        borderLeft: 'solid gray 1px',
    },
    "& th": { 
        borderBottom: 0 
    }
}

export const TimeTableRowStyle = {
    '&:first-of-type td': {
        pt: 5
    }
}

export const TimeTableBodyStyles = {
    display: 'grid'
}

export const TimeTableCellStyle = {
    width: '100vw',
    pb: 0,
    pt: 16
}

export const TableHeadCellStyle = {
    textAlign: 'center',
    fontSize: '20px'
}

export const TableMarkupStyle = {
    position: 'absolute',
    width: '96%'
}