import { useStore } from "../../api/stores/StoresManager";
import TimeTableMarkup from "../../components/markups/timeTable/TimeTableMarkup";
import { useEffect, useState } from "react";
import { ScheduleStartEndTime } from "../../models/Tenants";
import ScheduleTableBuilder from "./ScheduleTableBuilder";
import { useParams } from "react-router-dom";
import { useLoader } from "../../hooks/Loader";
import Loader from "../../components/hoc/loading/Loader";
import PageBox from "../../components/wrappers/PageBox";
import { observer } from "mobx-react-lite";

export const ScheduleTablePage = observer(() => {
    const { scheduleStore, userStore, tenantSettingsStore } = useStore();
    const { isTeacherScope, targetId } = useParams();
    const loader = useLoader();
    const [ timeTable, setTimeTable ] = useState<ScheduleStartEndTime[] | null>([]);

    useEffect(() => {
        const fetchTimeTable = async () => 
            await tenantSettingsStore.getTenantScheduleTimes()
                .then(res => setTimeTable(res))
                .then(() => loader.hide());

        loader.show();
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
                .then(() => loader.hide());

        fetchSchedules();
    }, [isTeacherScope, scheduleStore, targetId, userStore.user?.id]);

    return( 
        <PageBox>
            <TimeTableMarkup/>
            <Loader type='spin' replace>
                <ScheduleTableBuilder timeTable={timeTable} />
            </Loader>
        </PageBox>
    );
});