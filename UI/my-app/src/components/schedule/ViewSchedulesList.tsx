import { useEffect, useState } from "react";
import { useStore } from "../../api/stores/StoresManager";
import { noneWord, normalizeUserName } from "../../utils/Utils";
import { useCult } from "../../hooks/useTranslator";
import CustomSelect, { SelectItem } from "../CustomSelect";
import { buttonHoverStyles, buttonImageIconStyle } from "../../styles/ButtonStyles";
import { useNavigate } from "react-router-dom";
import FormHelperText from "@mui/material/FormHelperText";
import Button from "@mui/material/Button";
import Loader from "../hoc/loading/Loader";
import { useLoader } from "../../hooks/useLoader";
import EIcon from "../wrappers/EIcon";
import { observer } from "mobx-react-lite";

const ViewSchedulesList = () => {
    const { userStore, groupStore, tenantStore } = useStore();
    const navigate = useNavigate();
    const { translator } = useCult();
    const loader = useLoader();

    const [targetSchedule, setTargetSchedule] = useState({id: noneWord, type: ''});
    const [targetScheduleError, setTargetScheduleError] = useState('');

    useEffect(() => {
        const fetchGroups = async () => 
            await groupStore.getGroups()
                .then(() => fetchTeachers())
                .then(() => loader.hide());

        const fetchTeachers = async () => 
            await userStore.getTeachers();

        loader.show();
        fetchGroups();
    }, [tenantStore, userStore, groupStore]);

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
        const groupNames = groupStore.groups?.map(g => { 
            return { id: g.id, value: g.title, type: 'group' } 
        });

        const teacherNames = userStore.teachers?.map(t => { 
            return { id: t.id, value: normalizeUserName(t), type: 'teacher' } 
        });

        return groupNames?.concat(teacherNames) ?? null;
    }

    const errorHandler = () => {
        return(
            targetScheduleError && <FormHelperText sx={{color: 'red'}}>
                                    {targetScheduleError}
                                 </FormHelperText> 
        );
    }

    const openSchedule = () => {
        navigate(`/schedule/${targetSchedule.type}/${targetSchedule.id}`, {replace: false})
    }

    const updateCurrentValue = (item: string) => {
        const value = JSON.parse(item) as SelectItem;
        setTargetSchedule({id: value.id, type: value.type});
    }

    return(
        <Loader type='spin' replace>
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
                <EIcon type='add'/>
            </Button> 
        </Loader>
    );
}

export default observer(ViewSchedulesList);