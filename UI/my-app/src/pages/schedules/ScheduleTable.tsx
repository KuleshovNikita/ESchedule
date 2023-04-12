import { Box, Table, TableBody, TableCell, TableRow } from "@mui/material";
import { useStore } from "../../api/stores/StoresManager";
import TimeTableMarkup from "../../components/markups/timeTable/TimeTableMarkup";
import { ReactNode, useEffect, useState } from "react";
import { observer } from "mobx-react-lite";
import { DayOfWeek, ScheduleModel } from "../../models/Schedules";
import { daysOfWeek, timeTableScope } from "../../utils/Utils";
import { TimeTableBodyStyles } from "../../components/markups/timeTable/TimeTableMarkupStyles";
import { ScheduleItemStyle, ScheduleItemPlaceholderStyle, ScheduleRowStyle, ScheduleTableHeadStyle } from "./ScheduleTableStyles";
import ScheduleCellContent from "../../components/scheduleCell/ScheduleCellContent";
import LoadingComponent from "../../components/hoc/loading/LoadingComponent";

export const ScheduleTable = observer(() => {
    const { scheduleStore, userStore } = useStore();
    const [ isLoaded, setLoadedState ] = useState(false);
    let schedules: ScheduleModel[] | undefined;

    useEffect(() => {
        scheduleStore.getScheduleForTeacher(userStore.user?.id as string)
            .then(() => setLoadedState(true))
    }, [scheduleStore, userStore.user?.id]);

    const buildRowCells = () => {
            if(!schedules || schedules.length === 0) {
                return;
            }

            const schedule = schedules?.shift();
            const rowItems = schedules?.filter(sc => sc.startTime.getTime() === schedule?.startTime.getTime());
            rowItems?.push(schedule as ScheduleModel);
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

        schedules = scheduleStore.schedules?.slice()
                            .sort((sc1, sc2) => 
                                        sc1.startTime.getTime() - 
                                        sc2.startTime.getTime()
                                    );

        for(let i = timeTableScope.start; i <= timeTableScope.end; i++) {
            rows.push(
                <TableRow key={i} sx={ScheduleRowStyle}>
                    {schedules ? buildRowCells() : undefined}
                </TableRow>
            );
        }

        return rows;
    }

    return( 
        <Box>
            { 
                !isLoaded
            ?
                <LoadingComponent/>
            :
                <Box>
                    <TimeTableMarkup/>
                    <Table sx={ScheduleTableHeadStyle}>
                        <TableBody sx={TimeTableBodyStyles}>
                            { buildRows() }
                        </TableBody>
                    </Table>
                </Box>
            }
        </Box>
    );
})