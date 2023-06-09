import { Box } from "@mui/material";
import { useStore } from "../../api/stores/StoresManager";
import TimeTableMarkup from "../../components/markups/timeTable/TimeTableMarkup";
import { useEffect, useState } from "react";
import { ScheduleModel } from "../../models/Schedules";
import LoadingComponent from "../../components/hoc/loading/LoadingComponent";
import { ScheduleStartEndTime } from "../../models/Tenants";
import ScheduleTableBuilder from "./ScheduleTableBuilder";
import { useParams } from "react-router-dom";

export const ScheduleTablePage = () => {
    const { scheduleStore, userStore, tenantSettingsStore } = useStore();
    const { isTeacherScope, targetId } = useParams();
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
        const lowerScope = isTeacherScope?.toLocaleLowerCase();
        const normalizedScope = lowerScope && lowerScope === 'true';

        const fetchFromNecessarySource = async () => 
            normalizedScope
                ? await scheduleStore.getScheduleForTeacher(targetId as string)
                : await scheduleStore.getScheduleForGroup(targetId as string);
        

        const fetchSchedules = async () => {
            var res = await fetchFromNecessarySource();
            setSchedules(res);
            setLoadedState(true);
        }

        fetchSchedules();
    }, [isTeacherScope, scheduleStore, targetId, userStore.user?.id]);

    return( 
        <Box>
            <TimeTableMarkup/>
            { 
                !isLoaded
                    ? <LoadingComponent/>
                    : <ScheduleTableBuilder schedules={schedules} timeTable={timeTable} />
            }
        </Box>
    );
}