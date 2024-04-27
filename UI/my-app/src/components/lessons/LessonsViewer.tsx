import { useEffect, useState } from "react";
import { useStore } from "../../api/stores/StoresManager";
import { LessonCreateModel, LessonModel } from "../../models/Lessons";
import { Box, Button, Table, TableBody, TableCell, TableRow, TextField } from "@mui/material";
import LoadingComponent from "../hoc/loading/LoadingComponent";
import { buttonHoverStyles, buttonImageIconStyle } from "../../styles/ButtonStyles";
import { useCult } from "../../hooks/Translator";
import { toast } from "react-toastify";
import PopupForm from "../schedule/RulesCreation/PopupForm";
import AddCircleIcon from '@mui/icons-material/AddCircle';
import { Typography } from "@material-ui/core";
import DeleteIcon from '@mui/icons-material/Delete';
import SaveIcon from '@mui/icons-material/Save';

const cellStyle = {
    borderBottom: '1px solid black'
}

const removeBtnStyle = {
    ...cellStyle, 
    width: "20%",
    borderLeft: "1px solid black"
}

export default function LessonViewer() {
    const { tenantStore, lessonStore } = useStore();
    const { translator } = useCult();
    const [lessons, setLessons] = useState<LessonModel[]>([]);
    const [isModalActive, setModalActive] = useState(false);
    const [lessonName, setLessonName] = useState('');
    
    useEffect(() => {
        const fetchLessons = async () => 
            await tenantStore.getLessons(tenantStore.tenant?.id as string)
                .then(res => setLessons(res));

        fetchLessons();
    }, [tenantStore])

    const updateLessonsList = async () => {
        await lessonStore.updateLessonsList(lessons.map(x => x.id), tenantStore.tenant?.id as string)
                         .then(() => toast.success("toasts.lessons-list-updated"));
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
                    onChange={(e) => setLessonName(e.target.value)}
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

    const renderLessonsTable = () => {
        return  <Table sx={{width: '30%'}}>
                    <TableBody>
                        {
                            lessons.map((l, k) => {
                                return <TableRow key={k}>
                                    <TableCell sx={cellStyle}>
                                        <Typography variant="h6">
                                            {l.title}
                                        </Typography>
                                    </TableCell>
                                    <TableCell sx={removeBtnStyle}>
                                        <Button 
                                            onClick={() => setLessons(lessons.filter(les => les.id !== l.id))}
                                            variant='contained' 
                                            sx={buttonHoverStyles}>
                                                {translator('buttons.remove')}
                                                <DeleteIcon sx={buttonImageIconStyle}/>
                                        </Button>
                                    </TableCell>
                                </TableRow>
                            })
                        }
                    </TableBody>
                </Table>
    }

    return(<Box>
        {isModalActive && showModalWindow()}
        {lessons.length !== 0 ? renderLessonsTable() : <LoadingComponent type="circle"/>}
        <Button
            onClick={updateLessonsList}
            variant='contained' 
            sx={buttonHoverStyles}>
            {translator('buttons.save')}
            <SaveIcon sx={buttonImageIconStyle}/>
        </Button>
        <Button
            onClick={() => setModalActive(true)}
            variant='contained' 
            sx={{...buttonHoverStyles, ml: 1}}>
            {translator('buttons.add')}
            <AddCircleIcon sx={buttonImageIconStyle}/>
        </Button>
    </Box>);
}