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
    const { scope, targetId } = useParams();
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
        loader.show();

        const fetchSchedules = async () => {
            switch(scope?.toLocaleLowerCase()) {
                case 'teacher':
                    await scheduleStore.getScheduleForTeacher(targetId as string)
                    break;
                case 'group':
                    await scheduleStore.getScheduleForGroup(targetId as string)
                    break;

                default:
                    await scheduleStore.getScheduleForGroup(targetId as string)
                    break;
            }
        }                

        fetchSchedules()
            .then(() => loader.hide());
    }, [scope, scheduleStore, targetId, userStore.user?.id]);

    return( 
        <PageBox>
            <TimeTableMarkup/>
            <Loader type='spin' replace>
                <ScheduleTableBuilder timeRanges={timeTable} />
            </Loader>
        </PageBox>
    );
});