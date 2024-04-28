import { buttonHoverStyles, buttonImageIconStyle } from "../../styles/ButtonStyles";
import { useCult } from "../../hooks/Translator";
import PreviewIcon from '@mui/icons-material/Preview';
import { useState } from "react";
import PopupForm from "./RulesCreation/PopupForm";
import ViewSchedulesList from "./ViewSchedulesList";
import Button from "@mui/material/Button";

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