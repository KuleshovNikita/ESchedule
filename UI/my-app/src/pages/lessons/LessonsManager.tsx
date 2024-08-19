import { useEffect, useState } from "react";
import { useStore } from "../../api/stores/StoresManager";
import { LessonCreateModel, LessonModel } from "../../models/Lessons";
import { buttonHoverStyles } from "../../styles/ButtonStyles";
import { useCult } from "../../hooks/Translator";
import { toast } from "react-toastify";
import PopupForm from "../../components/modalWindow/PopupForm";
import { Typography } from "@material-ui/core";
import { observer } from "mobx-react-lite";
import TextField from "@mui/material/TextField";
import Button from "@mui/material/Button";
import TableBody from "@mui/material/TableBody";
import { useLoader } from "../../hooks/Loader";
import Loader from "../../components/hoc/loading/Loader";
import EIcon from "../../components/wrappers/EIcon";
import PageBox from "../../components/wrappers/PageBox";
import { Box, TableHead } from "@mui/material";
import { buttonsBox, pageMarkup } from "./LessonsManagerStyles";
import { ETableRow } from "../../components/wrappers/ETable/ETableRow";
import { ETableCell } from "../../components/wrappers/ETable/ETableCell";
import { ETable } from "../../components/wrappers/ETable/ETable";

const LessonsManager = observer(() => {
    const { tenantStore, lessonStore } = useStore();
    const { translator } = useCult();
    const [selectedlessons, setSelectedLessons] = useState<LessonModel[]>([]);
    const [isModalActive, setModalActive] = useState(false);
    const [lessonName, setLessonName] = useState('');
    const loader = useLoader();
    
    useEffect(() => {
        const fetchLessons = async () => 
            await lessonStore.getLessons()
                .then(res => lessonStore.lessons = res)
                .then(() => loader.hide())
                .catch(() => loader.hide());
        
        loader.show();
        fetchLessons();

    }, [tenantStore, lessonStore])

    const removeLessons = async () => {
        await lessonStore.removeLessons(selectedlessons.map(x => x.id))
            .then(() => toast.success(translator("toasts.lesson-removed")))
            .then(() => setSelectedLessons([]));
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
                    <EIcon type='save'/>
                </Button>
            </PopupForm>
        );
    }

    const handleCheck = (event: React.ChangeEvent<HTMLInputElement>, lesson: LessonModel) => {
        if(event.target.checked) {
            setSelectedLessons([...selectedlessons, lesson]);
        } else {
            setSelectedLessons(selectedlessons.filter(l => l.id !== lesson.id));
        }
    }

    const renderLessonsTable = () => {
        return(  
            <ETable tableName="labels.lessons-list">
                <TableHead>
                    <ETableRow headRow>
                        <ETableCell headCell/>
                        <ETableCell headCell columnName='labels.lesson-name'/>
                    </ETableRow>
                </TableHead>
                <TableBody>
                {
                    lessonStore.lessons?.map((l, k) => {
                        return (
                            <ETableRow key={k}>
                                <ETableCell kind='checkbox' onClick={(e) => handleCheck(e, l)}></ETableCell>
                                <ETableCell>
                                    <Typography variant="h6">
                                        {l.title}
                                    </Typography>
                                </ETableCell>
                            </ETableRow>
                        );
                    })
                }
                </TableBody>
            </ETable>
    )}

    return(
    <PageBox>
        <Box sx={pageMarkup}>
            <Box sx={buttonsBox}>
                <Button
                    disabled={selectedlessons.length === 0}
                    onClick={removeLessons}
                    variant='contained' 
                    sx={buttonHoverStyles}>
                    {translator('buttons.remove')}
                    <EIcon type='remove'/>
                </Button>
                <Button
                    onClick={() => setModalActive(true)}
                    variant='contained' 
                    sx={{...buttonHoverStyles, ml: 1}}>
                    {translator('buttons.add')}
                    <EIcon type='add'/>
                </Button>
            </Box>
            <Box>        
                {isModalActive && showModalWindow()}
                <Loader type="spin" replace>
                    {renderLessonsTable()}
                </Loader>
            </Box>
        </Box>
    </PageBox>
    );
});

export default LessonsManager;