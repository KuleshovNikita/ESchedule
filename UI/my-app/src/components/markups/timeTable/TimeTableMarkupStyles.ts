const TableRightMarginStyle = {
    mr: 5
}

export const RowAlignStyle = {
    display: "grid",
    gridTemplateColumns: "repeat(7, 1fr)" ,
    ml: 8,
    mt: 1,
}

export const TimeTableHeadStyles = {
    ...TableRightMarginStyle,
    ...RowAlignStyle,
    '*': {
        borderRight: 'solid gray 1px',
        padding: '5px',
        mr: 0.1,
        ml: 0.1
    },
}

export const TimeTableBodyStyles = {
    display: 'grid', 
    ...TableRightMarginStyle,
}

export const TimeTableCellStyle = {
    width: '100vw'
}

export const TableHeadCellStyle = {
    textAlign: 'center'
}

export const TableMarkupStyle = {
    position: 'absolute'
}