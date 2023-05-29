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
    const [popup, setPopup] = useState(false);
    const [lessonName, setLessonName] = useState('');
    
    useEffect(() => {
        const fetchLessons = async () => {
            var res = await tenantStore.getLessons(tenantStore.tenant?.id as string);
            setLessons(res);
        }

        fetchLessons();
    }, [tenantStore])

    const updateLessonsList = async () => {
        var response = await lessonStore.updateLessonsList(lessons.map(x => x.id), tenantStore.tenant?.id as string);

        if(response.isSuccessful) {
            toast.success("toasts.lessons-list-updated");
        }
    }

    const saveLesson = async () => {
        const lessonModel: LessonCreateModel = {
            title: lessonName,
            tenantId: tenantStore.tenant?.id as string
        }

        const response = await lessonStore.createLesson(lessonModel);

        if(response.isSuccessful) {
            toast.success(translator('toasts.lesson-added'));
        }

        setPopup(false);
    }

    const addSaveButton = () => {
        return (
            <Button sx={buttonHoverStyles}   
                    variant="contained"
                    disabled={lessonName === ''}
                    onClick={saveLesson}   
            >
                {translator('buttons.create')}
                <AddCircleIcon sx={buttonImageIconStyle}/>
            </Button>
        );
    }

    const showPopUp = () => {
        return (
            <PopupForm closeButtonHandler={() => setPopup(false)}>
                <TextField 
                    label={translator('labels.lesson-name')}
                    onChange={(e) => setLessonName(e.target.value)}
                />
                {addSaveButton()}
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
                                        </Button>
                                    </TableCell>
                                </TableRow>
                            })
                        }
                    </TableBody>
                </Table>
    }

    return(<Box>
        {popup && showPopUp()}
        {lessons.length !== 0 ? renderLessonsTable() : <LoadingComponent type="circle"/>}
        <Button
            onClick={updateLessonsList}
            variant='contained' 
            sx={buttonHoverStyles}>
            {translator('buttons.save')}
        </Button>
        <Button
            onClick={() => setPopup(true)}
            variant='contained' 
            sx={{...buttonHoverStyles, ml: 1}}>
            {translator('buttons.add-lesson')}
        </Button>
    </Box>);
}