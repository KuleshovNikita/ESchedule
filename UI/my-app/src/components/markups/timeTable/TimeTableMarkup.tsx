import { TimeTableHeadStyles, TableHeadCellStyle, TableMarkupStyle, TimeTableBodyStyles, TimeTableCellStyle, TimeTableRowStyle } from "./TimeTableMarkupStyles";
import { ReactNode } from "react";
import { daysOfWeek, timeTableScope } from "../../../utils/Utils";
import { useCult } from "../../../hooks/Translator";
import TableCell from "@mui/material/TableCell";
import TableRow from "@mui/material/TableRow";
import TableHead from "@mui/material/TableHead";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";

export default function TimeTableMarkup() {
    const { translator } = useCult();

    const buildTableHeadCells = () => {
        const rows: ReactNode[] = [];
    
        for(let i = 0; i < daysOfWeek.length; i++) {
            rows.push(
                <TableCell key={i} sx={TableHeadCellStyle}>
                    {translator('days-of-week.' + daysOfWeek[i])}
                </TableCell>
            );
        }
    
        return rows;
    }

    const buildTableHourRows = () => {
        const rows: ReactNode[] = [];
    
        for(let i = timeTableScope.start; i <= timeTableScope.end; i++) {
            rows.push(
                <TableRow key={i} sx={TimeTableRowStyle}>
                    <TableCell sx={TimeTableCellStyle}>
                        {i}:00
                    </TableCell>
                </TableRow>
            );
        }
    
        return rows;
    }

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