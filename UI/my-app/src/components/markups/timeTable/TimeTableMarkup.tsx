import { Table, TableBody, TableCell, TableHead, TableRow } from "@mui/material";
import { TimeTableHeadStyles, TableHeadCellStyle, TableMarkupStyle, TimeTableBodyStyles, TimeTableCellStyle } from "./TimeTableMarkupStyles";
import { ReactNode } from "react";
import { daysOfWeek, timeTableScope } from "../../../utils/Utils";

export default function TimeTableMarkup() {
    return(
        <Table sx={TableMarkupStyle}>
            <TableHead>
                <TableRow sx={TimeTableHeadStyles}>
                    { buildTableHeadCells() }
                </TableRow>
            </TableHead>
            <TableBody sx={TimeTableBodyStyles}>
                { buildTableHourRows() }
            </TableBody>
        </Table>
    );
}

const buildTableHourRows = () => {
    const rows: ReactNode[] = [];

    for(let i = timeTableScope.start; i <= timeTableScope.end; i++) {
        rows.push(
            <TableRow key={i}>
                <TableCell sx={TimeTableCellStyle}>
                    {i}:00
                </TableCell>
            </TableRow>
        );
    }

    return rows;
}

const buildTableHeadCells = () => {
    const rows: ReactNode[] = [];

    for(let i = 0; i < daysOfWeek.length; i++) {
        rows.push(
            <TableCell key={i} sx={TableHeadCellStyle}>
                {daysOfWeek[i]}
            </TableCell>
        );
    }

    return rows;
}

