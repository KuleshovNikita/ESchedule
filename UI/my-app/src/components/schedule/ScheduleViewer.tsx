import { buttonHoverStyles, buttonImageIconStyle } from "../../styles/ButtonStyles";
import { useCult } from "../../hooks/useTranslator";
import { useState } from "react";
import PopupForm from "../modalWindow/PopupForm";
import ViewSchedulesList from "./ViewSchedulesList";
import Button from "@mui/material/Button";
import EIcon from "../wrappers/EIcon";

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
            <EIcon type='preview'/>
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