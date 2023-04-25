import { Box, Button } from "@mui/material";
import RuleSelect from "./RuleSelect";
import CloseIcon from '@mui/icons-material/Close';
import { newRuleFormStyle, 
         outerTransperancyStyle, 
         ruleBodyFormStyle, 
         ruleFormCloseButtonStyle } from "../../../pages/schedules/ScheduleTableStyles";

interface Props {
    closeAction: any
}

export default function CreateRuleForm({closeAction}: Props) {
    return(
        <Box sx={outerTransperancyStyle}>
            <Box sx={newRuleFormStyle}>
                <Box sx={{mb: 5}}>
                    <Button sx={ruleFormCloseButtonStyle} onClick={() => closeAction()} >
                        <CloseIcon />
                    </Button>
                </Box>
                <Box sx={ruleBodyFormStyle}>
                    <RuleSelect onConfirm={() => closeAction()}/>
                </Box>
            </Box>
        </Box>
    );
}