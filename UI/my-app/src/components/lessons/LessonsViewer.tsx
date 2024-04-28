import { useEffect, useState } from "react";
import { useStore } from "../../api/stores/StoresManager";
import { LessonCreateModel, LessonModel } from "../../models/Lessons";
import LoadingComponent from "../hoc/loading/LoadingComponent";
import { buttonHoverStyles, buttonImageIconStyle } from "../../styles/ButtonStyles";
import { useCult } from "../../hooks/Translator";
import { toast } from "react-toastify";
import PopupForm from "../schedule/RulesCreation/PopupForm";
import AddCircleIcon from '@mui/icons-material/AddCircle';
import { Typography } from "@material-ui/core";
import DeleteIcon from '@mui/icons-material/Delete';
import SaveIcon from '@mui/icons-material/Save';
import Checkbox from '@mui/material/Checkbox';
import { observer } from "mobx-react-lite";
import TextField from "@mui/material/TextField";
import Button from "@mui/material/Button";
import TableBody from "@mui/material/TableBody";
import Table from "@mui/material/Table";
import TableCell from "@mui/material/TableCell";
import TableRow from "@mui/material/TableRow";
import Box from "@mui/material/Box";

const cellStyle = {
    border: '1px solid black'
}

const checkboxStyle = {
    '& .MuiSvgIcon-root': { fontSize: 40 }
}

const LessonViewer = observer(() => {
    const { tenantStore, lessonStore } = useStore();
    const { translator } = useCult();
    const [lessonsToRemove, setLessonsToRemove] = useState<LessonModel[]>([]);
    const [isModalActive, setModalActive] = useState(false);
    const [lessonName, setLessonName] = useState('');
    
    useEffect(() => {
        const fetchLessons = async () => 
            await tenantStore.getLessons(tenantStore.tenant?.id as string)
                .then(res => lessonStore.lessons = res);

        fetchLessons();
    }, [tenantStore, lessonStore])

    const removeLessons = async () => {
        await lessonStore.removeLessons(lessonsToRemove.map(x => x.id), tenantStore.tenant?.id as string)
            .then(() => toast.success(translator("toasts.lesson-removed")));
    }

    const saveLesson = async () => {
        const lessonModel: LessonCreateModel = {
            title: lessonName,
            tenantId: tenantStore.tenant?.id as string
        }

        await lessonStore.createLesson(lessonModel)
            .then(() => toast.success(translator('toasts.lesson-added')));

        setModalActive(false);
    }

    const showModalWindow = () => {
        return (
            <PopupForm closeButtonHandler={() => setModalActive(false)}>
                <TextField 
                    label={translator('labels.lesson-name')}
                    onChange={(e: any) => setLessonName(e.target.value)}
                />
                <Button sx={buttonHoverStyles}   
                    variant="contained"
                    disabled={lessonName === ''}
                    onClick={saveLesson}   
                >
                    {translator('buttons.create')}
                    <SaveIcon/>
                    <AddCircleIcon sx={buttonImageIconStyle}/>
                </Button>
            </PopupForm>
        );
    }

    const handleCheck = (event: React.ChangeEvent<HTMLInputElement>, lesson: LessonModel) => {
        if(event.target.checked) {
            setLessonsToRemove([...lessonsToRemove, lesson]);
        } else {
            setLessonsToRemove(lessonsToRemove.filter(l => l.id !== lesson.id));
        }
    }

    const renderLessonsTable = () => {
        return  <Table sx={{display: 'inline-block'}}>
                    <TableBody>
                        {
                            lessonStore.lessons!.map((l, k) => {
                                return <TableRow key={k}>
                                    <TableCell sx={{...cellStyle, padding: '5px', width: '0%'}}>
                                        <Checkbox 
                                            sx={checkboxStyle}
                                            onChange={(e) => handleCheck(e, l)}/>                                        
                                    </TableCell>
                                    <TableCell sx={cellStyle}>
                                        <Typography variant="h6">
                                            {l.title}
                                        </Typography>
                                    </TableCell>
                                </TableRow>
                            })
                        }
                    </TableBody>
                </Table>
    }

    return(<Box>
        {isModalActive && showModalWindow()}
        {lessonStore.lessons !== null ? renderLessonsTable() : <LoadingComponent type="circle"/>}
        <Button
            disabled={lessonsToRemove.length === 0}
            onClick={removeLessons}
            variant='contained' 
            sx={buttonHoverStyles}>
            {translator('buttons.remove')}
            <DeleteIcon sx={buttonImageIconStyle}/>
        </Button>
        <Button
            onClick={() => setModalActive(true)}
            variant='contained' 
            sx={{...buttonHoverStyles, ml: 1}}>
            {translator('buttons.add')}
            <AddCircleIcon sx={buttonImageIconStyle}/>
        </Button>
    </Box>);
});

export default LessonViewer;