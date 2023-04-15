import { Box } from "@mui/material";
import { useStore } from "../../api/stores/StoresManager";
import TimeTableMarkup from "../../components/markups/timeTable/TimeTableMarkup";
import { useEffect, useState } from "react";
import { ScheduleModel } from "../../models/Schedules";
import LoadingComponent from "../../components/hoc/loading/LoadingComponent";
import { ScheduleStartEndTime } from "../../models/Tenants";
import ScheduleTableBuilder from "./ScheduleTableBuilder";

export const ScheduleTablePage = () => {
    const { scheduleStore, userStore, tenantSettingsStore } = useStore();
    const [ isLoaded, setLoadedState ] = useState(false);
    const [ schedules, setSchedules ] = useState<ScheduleModel[]>([]);
    const [ timeTable, setTimeTable ] = useState<ScheduleStartEndTime[]>([]);

    useEffect(() => {
        const fetchTimeTable = async () => {
            var res = await tenantSettingsStore.getTenantScheduleTimes(userStore.user?.tenantId as string);
            setTimeTable(res);
        }

        fetchTimeTable();
    }, [tenantSettingsStore, userStore.user?.tenantId])

    useEffect(() => {
        const fetchSchedules = async () => {
            var res = await scheduleStore.getScheduleForTeacher(userStore.user?.id as string);
            setSchedules(res);
            setLoadedState(true);
        }

        fetchSchedules();
    }, [scheduleStore, userStore.user?.id]);

    return( 
        <Box>
            <TimeTableMarkup/>
            { 
                !isLoaded
            ?
                <LoadingComponent/>
            :
                <ScheduleTableBuilder schedules={schedules} timeTable={timeTable} />
            }
        </Box>
    );
}