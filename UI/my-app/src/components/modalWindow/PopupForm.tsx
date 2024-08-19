import { newRuleFormStyle, 
         outerTransperancyStyle, 
         ruleBodyFormStyle, 
         ruleFormCloseButtonStyle } from "../../pages/schedules/ScheduleTableStyles";
import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import EIcon from '../wrappers/EIcon';

interface Props {
    closeButtonHandler: any,
    children: any
}

export default function PopupForm({closeButtonHandler, children}: Props) {
    return(
        <Box sx={outerTransperancyStyle}>
            <Box sx={newRuleFormStyle}>
                <Box sx={{mb: 1}}>
                    <Button sx={ruleFormCloseButtonStyle} onClick={() => closeButtonHandler()} >
                        <EIcon sx={{pl: 0}} type='close'/>
                    </Button>
                </Box>
                <Box sx={ruleBodyFormStyle}>
                    {children}
                </Box>
            </Box>
        </Box>
    );
}