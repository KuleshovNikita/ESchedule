import { useStore } from "../../api/stores/StoresManager";
import TimeTableMarkup from "../../components/markups/timeTable/TimeTableMarkup";
import { useEffect, useState } from "react";
import { ScheduleModel } from "../../models/Schedules";
import { ScheduleStartEndTime } from "../../models/Tenants";
import ScheduleTableBuilder from "./ScheduleTableBuilder";
import { useParams } from "react-router-dom";
import Box from "@mui/material/Box";
import { useLoader } from "../../hooks/Loader";
import Loader from "../../components/hoc/loading/Loader";

export const ScheduleTablePage = () => {
    const { scheduleStore, userStore, tenantSettingsStore } = useStore();
    const { isTeacherScope, targetId } = useParams();
    const loader = useLoader();
    const [ schedules, setSchedules ] = useState<ScheduleModel[]>([]);
    const [ timeTable, setTimeTable ] = useState<ScheduleStartEndTime[]>([]);

    useEffect(() => {
        const fetchTimeTable = async () => 
            await tenantSettingsStore.getTenantScheduleTimes()
                .then(res => setTimeTable(res));

        fetchTimeTable();
    }, [tenantSettingsStore])

    useEffect(() => {
        const lowerScope = isTeacherScope?.toLocaleLowerCase();
        const normalizedScope = lowerScope && lowerScope === 'true';

        loader.show();

        const fetchFromNecessarySource = async () => 
            normalizedScope
                ? await scheduleStore.getScheduleForTeacher(targetId as string)
                : await scheduleStore.getScheduleForGroup(targetId as string);
        

        const fetchSchedules = async () => 
            await fetchFromNecessarySource()
                .then(res => setSchedules(res))
                .then(() => loader.hide());

        fetchSchedules();
    }, [isTeacherScope, scheduleStore, targetId, userStore.user?.id]);

    return( 
        <Box>
            <TimeTableMarkup/>
            <Loader type='spin' replace>
                <ScheduleTableBuilder schedules={schedules} timeTable={timeTable} />
            </Loader>
        </Box>
    );
}