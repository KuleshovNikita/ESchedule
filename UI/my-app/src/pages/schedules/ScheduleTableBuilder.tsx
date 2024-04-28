import { ReactNode } from "react";
import { ScheduleStartEndTime } from "../../models/Tenants";
import { ScheduleItemPlaceholderStyle, ScheduleItemStyle, ScheduleRowStyle, ScheduleTableHeadStyle } from "./ScheduleTableStyles";
import ScheduleCellContent from "../../components/schedule/ScheduleCellContent";
import { DayOfWeek, ScheduleModel } from "../../models/Schedules";
import { daysOfWeek, timeTableScope } from "../../utils/Utils";
import { TimeTableBodyStyles } from "../../components/markups/timeTable/TimeTableMarkupStyles";
import TableCell from "@mui/material/TableCell";
import TableRow from "@mui/material/TableRow";
import TableBody from "@mui/material/TableBody";
import Table from "@mui/material/Table";

interface Props {
    schedules: ScheduleModel[],
    timeTable: ScheduleStartEndTime[]
}

export default function ScheduleTableBuilder({ schedules, timeTable }: Props) {

    const buildRowCells = (timeRange: ScheduleStartEndTime) => {
        if(!schedules || schedules.length === 0) {
            return;
        }

        const rowItems = schedules?.filter(x => x.startTime.getTime() === timeRange.startTime.getTime());
        const result: ReactNode[] = [];

        for(let i = 0; i < daysOfWeek.length; i++) {
            const item = rowItems.find(x => x.dayOfWeek === i as DayOfWeek);

            if(item) {
                result.push(<TableCell sx={ScheduleItemStyle} key={item.id}>
                                <ScheduleCellContent item={item}/>
                            </TableCell>);
            } else {
                result.push(<TableCell sx={ScheduleItemPlaceholderStyle} key={i}></TableCell>)
            } 
        }

        schedules = schedules?.filter(sc => !rowItems?.includes(sc));

    return result;
}

const buildRows = () => {
    const rows: ReactNode[] = [];    

    schedules = schedules.sort((sc1, sc2) => 
                            sc1.startTime.getTime() - 
                            sc2.startTime.getTime()
                        );
                        

    for(let j = 0, i = timeTableScope.start; i <= timeTableScope.end; i++, j++) {
        rows.push(
            <TableRow key={i} sx={ScheduleRowStyle}>
                {schedules ? buildRowCells(timeTable![j]) : undefined}
            </TableRow>
        );
    }

    return rows;
}

    return(
        <Table sx={ScheduleTableHeadStyle}>
            <TableBody sx={TimeTableBodyStyles}>
                { buildRows() }
            </TableBody>
        </Table>
    );
}