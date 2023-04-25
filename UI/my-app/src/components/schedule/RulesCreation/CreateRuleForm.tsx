import { Box, Button } from "@mui/material";
import RuleSelect from "./RuleSelect";
import CloseIcon from '@mui/icons-material/Close';
import SaveIcon from '@mui/icons-material/Save';
import { newRuleFormStyle, 
         outerTransperancyStyle, 
         ruleBodyFormStyle, 
         ruleFormCloseButtonStyle } from "../../../pages/schedules/ScheduleTableStyles";
import { useCult } from "../../../hooks/Translator";
import { buttonHoverStyles, buttonImageIconStyle } from "../../../styles/ButtonStyles";
import { useState } from "react";

interface Props {
    closeAction: any
}

export default function CreateRuleForm({closeAction}: Props) {
    const {translator} = useCult();
    const [disableSaveButton, setSaveButtonEnable] = useState(true);

    const switchSaveButton = (val: boolean) => {
        setSaveButtonEnable(val);
    }

    return(
        <Box sx={outerTransperancyStyle}>
            <Box sx={newRuleFormStyle}>
                <Box sx={{mb: 5}}>
                    <Button sx={ruleFormCloseButtonStyle} onClick={() => closeAction()} >
                        <CloseIcon />
                    </Button>
                </Box>
                <Box sx={ruleBodyFormStyle}>
                    <RuleSelect switchSaveButton={switchSaveButton}/>
                    <Button sx={buttonHoverStyles}   
                            variant="contained"
                            disabled={disableSaveButton}   
                    >
                        {translator('buttons.save')}
                        <SaveIcon sx={buttonImageIconStyle}/>
                    </Button>
                </Box>
            </Box>
        </Box>
    );
}