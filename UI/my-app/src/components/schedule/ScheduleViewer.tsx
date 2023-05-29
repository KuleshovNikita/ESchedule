import { buttonHoverStyles, buttonImageIconStyle } from "../../styles/ButtonStyles";
import { useCult } from "../../hooks/Translator";
import { Button } from "@mui/material";
import PreviewIcon from '@mui/icons-material/Preview';
import { useState } from "react";
import PopupForm from "./RulesCreation/PopupForm";
import ViewSchedulesList from "./ViewSchedulesList";

export default function ScheduleViewer() {
    const {translator} = useCult();
    const [formEnabled, setFormEnabled] = useState(false);

    const showForm = () => {
        setFormEnabled(!formEnabled);
    }

    return(<>
        <Button sx={buttonHoverStyles} 
                variant="contained"
                onClick={showForm}
        >
            {translator('buttons.view-schedules')}
            <PreviewIcon sx={buttonImageIconStyle}/>
        </Button>

        {
            formEnabled 
        &&
            <PopupForm closeButtonHandler={showForm}>
                <ViewSchedulesList/>
            </PopupForm>
        }
    </>)
} 