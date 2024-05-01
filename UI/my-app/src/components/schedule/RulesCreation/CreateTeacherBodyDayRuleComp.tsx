import { useEffect, useState } from "react";
import { UserModel } from "../../../models/Users";
import { useStore } from "../../../api/stores/StoresManager";
import { useCult } from "../../../hooks/Translator";
import { daysOfWeek, noneWord, normalizeUserName } from "../../../utils/Utils";
import CustomSelect from "../../CustomSelect";
import FormHelperText from "@mui/material/FormHelperText";
import Loader from "../../hoc/loading/Loader";
import { useLoader } from "../../../hooks/Loader";

interface Props {
    setHasErrors: any,
    bodyData: any
}

export default function CreateTeacherBodyDayRuleComp({setHasErrors, bodyData}: Props) {
    const { userStore } = useStore();
    const { translator } = useCult();
    const [teachersInfo, setTeachersInfo] = useState<UserModel[]>([]);
    const [isLoaded, setLoaded] = useState(false);
    const loader = useLoader();

    const [busyTeacher, setBusyTeacher] = useState(noneWord);
    const [busyTeacherError, setBusyTeacherError] = useState('');

    const [day, setDay] = useState(noneWord);
    const [dayError, setDayError] = useState('');

    useEffect(() => {
        const fetchTeachers = async () => 
            await userStore.getTeachers()
                .then(res => setTeachersInfo(res))
                .then(() => loader.hide());

        if(teachersInfo.length === 0) {
            loader.show();
            fetchTeachers();
        }  
    }, [teachersInfo]);

    useEffect(() => {
        if(busyTeacher === noneWord) {
            setBusyTeacherError(translator('input-helpers.invalid-value'));
            setHasErrors(true);
        } else {
            bodyData.ActorId = busyTeacher;
            setBusyTeacherError('');
        }

        if(day === noneWord) {
            setDayError(translator('input-helpers.invalid-value'));
            setHasErrors(true);
        } else {
            bodyData.Target = day;
            setDayError('');
        }

        if(!dayError && !busyTeacherError) {
            setHasErrors(false);
        }

    }, [bodyData, busyTeacher, busyTeacherError, day, dayError, setHasErrors, translator]);

    const busyTeacherErrorHandler = () => {
        return (
            busyTeacherError && <FormHelperText sx={{color: 'red'}}>
                                    {busyTeacherError}
                                </FormHelperText> 
        );
    }

    const dayErrorHandler = () => {
        return (
            dayError && <FormHelperText sx={{color: 'red'}}>
                            {dayError}
                        </FormHelperText> 
        );
    }

    const normalizeDaysList = () => {
        return daysOfWeek.map((x, k) => { 
            return {
                id: k.toString(), 
                value: translator(`days-of-week.${x}`), 
                type: 'day'
            } 
        });
    }

    const normalizeTeacherList = () => {
        return teachersInfo.map(x => { 
            return {
                id: x.id, 
                value: normalizeUserName(x), 
                type: 'teacher'
            } 
        });
    }

    const getId = (item: string) => {
        if(item === noneWord) {
            return noneWord;
        }

        return JSON.parse(item).id as string;
    }
   
    return (
        <Loader type='spin' replace>
            <CustomSelect 
                label={translator('labels.busy-teacher')} 
                onChange={e => setBusyTeacher(getId(e.target.value))} 
                collection={normalizeTeacherList()} 
                errorHandler={busyTeacherErrorHandler} />

            <CustomSelect 
                label={translator('labels.day-of-week')} 
                onChange={e => setDay(getId(e.target.value))} 
                collection={normalizeDaysList()} 
                errorHandler={dayErrorHandler} />
        </Loader>
    );
}