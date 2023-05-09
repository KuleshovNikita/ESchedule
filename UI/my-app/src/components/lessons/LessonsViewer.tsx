import { useEffect, useState } from "react";
import { useStore } from "../../api/stores/StoresManager";
import { LessonModel } from "../../models/Lessons";
import { Box, Button, Table, TableBody, TableCell, TableRow } from "@mui/material";
import LoadingComponent from "../hoc/loading/LoadingComponent";
import { buttonHoverStyles } from "../../styles/ButtonStyles";
import { useCult } from "../../hooks/Translator";
import { toast } from "react-toastify";

export default function LessonViewer() {
    const { tenantStore, lessonStore } = useStore();
    const { translator } = useCult();
    const [lessons, setLessons] = useState<LessonModel[]>([]);
    
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
            toast("toasts.lessons-list-updated");
        }
    }

    const renderLessonsTable = () => {
        return  <Table sx={{width: '30%'}}>
                    <TableBody>
                        {
                            lessons.map((l, k) => {
                                return <TableRow key={k}>
                                    <TableCell>
                                        {l.title}
                                    </TableCell>
                                    <TableCell>
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
        {lessons.length !== 0 ? renderLessonsTable() : <LoadingComponent type="circle"/>}
        <Button
            onClick={updateLessonsList}
            variant='contained' 
            sx={buttonHoverStyles}>
            {translator('buttons.save')}
        </Button>
    </Box>);
}