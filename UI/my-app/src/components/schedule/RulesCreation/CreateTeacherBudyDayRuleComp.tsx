import { useEffect, useState } from "react";
import { UserModel } from "../../../models/Users";
import { useStore } from "../../../api/stores/StoresManager";
import { FormHelperText, InputLabel, MenuItem, Select } from "@mui/material";
import LoadingComponent from "../../hoc/loading/LoadingComponent";
import { useCult } from "../../../hooks/Translator";
import { daysOfWeek, noneWord } from "../../../utils/Utils";

const style = {
    mt: 1, 
}

interface Props {
    setHasErrors: any,
    bodyData: any
}

export default function CreateTeacherBodyDayRuleComp({setHasErrors, bodyData}: Props) {
    const { tenantStore, userStore } = useStore();
    const { translator } = useCult();
    const [teachersInfo, setTeachersInfo] = useState<UserModel[]>([]);
    const [isLoaded, setLoaded] = useState(false);

    const [busyTeacher, setBusyTeacher] = useState(noneWord);
    const [busyTeacherError, setBusyTeacherError] = useState('');

    const [day, setDay] = useState(noneWord);
    const [dayError, setDayError] = useState('');

    useEffect(() => {
        const fetchTeachers = async () => {
            const res = await tenantStore.getTeachers(tenantStore.tenant?.id!);
            setLoaded(true);
            setTeachersInfo(res);
        }

        const fetchTenant = async () => {
            await tenantStore.getTenant(userStore.user?.tenantId!);
        }

        fetchTenant();

        if(teachersInfo.length === 0) {
            fetchTeachers();
        }  
    }, [teachersInfo, tenantStore, userStore.user?.tenantId]);

    useEffect(() => {
        if(busyTeacher === noneWord) {
            setBusyTeacherError(translator('errors.invalid-value'));
            setHasErrors(true);
        } else {
            bodyData.busyTeacher = busyTeacher;
            setBusyTeacherError('');
        }

        if(day === noneWord) {
            setDayError(translator('errors.invalid-value'));
            setHasErrors(true);
        } else {
            bodyData.day = day;
            setDayError('');
        }

        if(!dayError && !busyTeacherError) {
            setHasErrors(false);
        }

    }, [bodyData, busyTeacher, busyTeacherError, day, dayError, setHasErrors, translator]);

    const normalizeName = (user: UserModel) => 
        `${user.lastName} ${user.lastName[0]}. ${user.fatherName[0]}.`;
   
    return (
        <>
        {
            !isLoaded
        ? 
            <LoadingComponent type='circle'/>
        :
            <>
                <InputLabel sx={style}>{translator('labels.busy-teacher')}</InputLabel>
                <Select defaultValue={noneWord}
                        onChange={e => setBusyTeacher(e.target.value)}
                >
                    <MenuItem key={-1} value={noneWord}>{translator(noneWord)}</MenuItem>
                    {teachersInfo?.map((v, k) => {
                        return <MenuItem key={k} value={v.id}>{normalizeName(v)}</MenuItem>
                    })}
                </Select>
                { 
                    busyTeacherError && <FormHelperText sx={{color: 'red'}}>
                                            {busyTeacherError}
                                        </FormHelperText> 
                }

                <InputLabel sx={style}>{translator('labels.day-of-week')}</InputLabel>
                <Select defaultValue={noneWord}
                        onChange={e => setDay(e.target.value)}
                >
                    <MenuItem key={-1} value={noneWord}>{translator(noneWord)}</MenuItem>
                    {daysOfWeek?.map((v, k) => {
                        return <MenuItem key={k} value={k}>{translator(`days-of-week.${v}`)}</MenuItem>
                    })}
                </Select>
                { 
                    dayError && <FormHelperText sx={{color: 'red'}}>
                                            {dayError}
                                </FormHelperText> 
                }
            </>
        }
        </>
    );
}