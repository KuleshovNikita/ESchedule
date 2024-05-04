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
import { useStore } from "../../api/stores/StoresManager";

export default function ScheduleTableBuilder() {
    const { scheduleStore, tenantSettingsStore } = useStore();
    let schedules: ScheduleModel[] | null;

    const defineStyle = (item: ScheduleModel | undefined) => 
        item 
         ? ScheduleItemStyle
         : ScheduleItemPlaceholderStyle;

    const buildRowCells = (timeRange: ScheduleStartEndTime, rowNumber: number) => {
        if(!schedules || schedules.length === 0) {
            return;
        }

        const rowItems = schedules?.filter(x => x.startTime.getTime() === timeRange.startTime.getTime());
        const result: ReactNode[] = [];

        for(let cellNumber = 0; cellNumber < daysOfWeek.length; cellNumber++) {
            const item = rowItems.find(x => x.dayOfWeek === cellNumber as DayOfWeek);
            const key = `${rowNumber}${cellNumber}`

            result.push(
                <TableCell sx={defineStyle(item)} key={key}>
                    <ScheduleCellContent item={item}/>
                </TableCell>
            );
        }

        schedules = schedules?.filter(sc => !rowItems?.includes(sc));

        return result;
    }

    const buildRows = () => {
        const rows: ReactNode[] = [];  
        const timeRanges = tenantSettingsStore.timeTableList;  

        schedules = scheduleStore.schedules?.sort((sc1, sc2) => 
                                sc1.startTime.getTime() - 
                                sc2.startTime.getTime()
                            ) ?? null;
                            

        for(let j = 0, row = timeTableScope.start; row <= timeTableScope.end; row++, j++) {
            rows.push(
                <TableRow key={row} sx={ScheduleRowStyle}>
                    {schedules ? buildRowCells(timeRanges![j], row) : <></>}
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