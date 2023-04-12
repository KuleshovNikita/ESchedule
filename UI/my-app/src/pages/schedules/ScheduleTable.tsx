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
import { ScheduleStartEndTime } from "../../models/Tenants";

export const ScheduleTable = observer(() => {
    const { scheduleStore, userStore, tenantSettingsStore } = useStore();
    const [ isLoaded, setLoadedState ] = useState(false);
    let schedules: ScheduleModel[] | undefined;
    let timeTable: ScheduleStartEndTime[] | undefined;

    useEffect(() => {

        const fetchSettings = async () => {
            await tenantSettingsStore.getTenantScheduleTimes(userStore.user?.tenantId as string);
        } 
        
        const fetchSchedules = async () => {
            await scheduleStore.getScheduleForTeacher(userStore.user?.id as string)
                    .then(() => setLoadedState(true));
        }

        fetchSettings();
        fetchSchedules();
    }, [scheduleStore, tenantSettingsStore, userStore.user?.id, userStore.user?.tenantId]);

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
        
        timeTable = tenantSettingsStore.timeTableList?.slice();

        schedules = scheduleStore.schedules?.slice()
                            .sort((sc1, sc2) => 
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
        <Box>
            <TimeTableMarkup/>
            { 
                !isLoaded
            ?
                <LoadingComponent/>
            :
                <Table sx={ScheduleTableHeadStyle}>
                    <TableBody sx={TimeTableBodyStyles}>
                        { buildRows() }
                    </TableBody>
                </Table>
            }
        </Box>
    );
})