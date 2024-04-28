import CloseIcon from '@mui/icons-material/Close';
import { newRuleFormStyle, 
         outerTransperancyStyle, 
         ruleBodyFormStyle, 
         ruleFormCloseButtonStyle } from "../../../pages/schedules/ScheduleTableStyles";
import Box from '@mui/material/Box';
import Button from '@mui/material/Button';

interface Props {
    closeButtonHandler: any,
    children: any
}

export default function PopupForm({closeButtonHandler, children}: Props) {
    return(
        <Box sx={outerTransperancyStyle}>
            <Box sx={newRuleFormStyle}>
                <Box sx={{mb: 5}}>
                    <Button sx={ruleFormCloseButtonStyle} onClick={() => closeButtonHandler()} >
                        <CloseIcon />
                    </Button>
                </Box>
                <Box sx={ruleBodyFormStyle}>
                    {children}
                </Box>
            </Box>
        </Box>
    );
}