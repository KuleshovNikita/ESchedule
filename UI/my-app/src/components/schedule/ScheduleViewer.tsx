import { buttonHoverStyles, buttonImageIconStyle } from "../../styles/ButtonStyles";
import { useCult } from "../../hooks/Translator";
import { useState } from "react";
import PopupForm from "./RulesCreation/PopupForm";
import ViewSchedulesList from "./ViewSchedulesList";
import Button from "@mui/material/Button";
import Icon from "../wrappers/Icon";

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
            <Icon type='preview'/>
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