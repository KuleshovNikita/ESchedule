import { useEffect, useState } from "react";
import { useStore } from "../../api/stores/StoresManager";
import { Button, FormHelperText } from "@mui/material";
import { noneWord, normalizeUserName } from "../../utils/Utils";
import { useCult } from "../../hooks/Translator";
import CustomSelect, { SelectItem } from "../CustomSelect";
import AddCircleIcon from '@mui/icons-material/AddCircle';
import LoadingComponent from "../hoc/loading/LoadingComponent";
import { buttonHoverStyles, buttonImageIconStyle } from "../../styles/ButtonStyles";
import { useNavigate } from "react-router-dom";

export default function ViewSchedulesList() {
    const { tenantStore, userStore } = useStore();
    const navigate = useNavigate();
    const { translator } = useCult();
    const [isLoaded, setLoaded] = useState(false);

    const [targetSchedule, setTargetSchedule] = useState({id: noneWord, type: ''});
    const [targetScheduleError, setTargetScheduleError] = useState('');

    useEffect(() => {
        const fetchGroups = async () => 
            await tenantStore.getGroups(userStore.user?.tenantId as string);

        const fetchTeachers = async () => 
            await tenantStore.getTeachers(userStore.user?.tenantId as string)
                .then(() => setLoaded(true));

        fetchGroups();
        fetchTeachers();
    }, [tenantStore, userStore.user?.tenantId]);

    useEffect(() => {
        if(targetSchedule.id === noneWord) {
            setTargetScheduleError(translator('input-helpers.schedule-not-selected'));
        } else if (targetSchedule.id !== noneWord && targetScheduleError === '') {
            //do nothing
        } else {
            setTargetScheduleError('');
        }
    }, [targetSchedule, targetScheduleError, translator]);

    const getCollection = () => {
        const groupNames = tenantStore.tenantGroups.map(g => { 
            return { id: g.id, value: g.title, type: 'group' } 
        });

        const teacherNames = tenantStore.tenantTeachers.map(t => { 
            return { id: t.id, value: normalizeUserName(t), type: 'teacher' } 
        });

        return groupNames.concat(teacherNames);
    }

    const errorHandler = () => {
        return(
            targetScheduleError && <FormHelperText sx={{color: 'red'}}>
                                    {targetScheduleError}
                                 </FormHelperText> 
        );
    }

    const openSchedule = () => {
        navigate(`/schedule/${targetSchedule.type === 'teacher'}/${targetSchedule.id}`, {replace: false})
    }

    const updateCurrentValue = (item: string) => {
        const value = JSON.parse(item) as SelectItem;
        setTargetSchedule({id: value.id, type: value.type as string});
    }

    return(<>
    {
        !isLoaded
    ? 
        <LoadingComponent type='circle'/>
    :
        <>
            <CustomSelect 
                label={translator('labels.target-schedule')} 
                onChange={e => updateCurrentValue(e.target.value)} 
                collection={getCollection()} 
                errorHandler={errorHandler} 
            />
            
            <Button sx={buttonHoverStyles}   
                    variant="contained"
                    disabled={targetScheduleError !== ''}
                    onClick={openSchedule}   
            >
                {translator('buttons.open')}
                <AddCircleIcon sx={buttonImageIconStyle}/>
            </Button>
        </>
    }
    </>);
}