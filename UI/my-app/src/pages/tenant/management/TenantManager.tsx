import { useCult } from "../../../hooks/Translator";
import { Typography } from "@material-ui/core";
import Box from "@mui/material/Box";
import { Button } from "@mui/material";
import EIcon from "../../../components/wrappers/EIcon";
import { btnStyle, iconStyle, pageStyle } from "./TenantManagerStyles";
import { useNavigate } from "react-router-dom";
import PageBox from "../../../components/wrappers/PageBox";

export default function TenantManager() {
    const { translator } = useCult();
    const navigate = useNavigate();

    return (
        <PageBox>
            <Box sx={pageStyle}>
                <Button sx={btnStyle} onClick={() => navigate('/rulesManagement')}>
                    <EIcon type='edit' sx={iconStyle}/>
                    <Typography>
                        {translator('labels.rules-list')}
                    </Typography>
                </Button>

                <Button sx={btnStyle} onClick={() => navigate('/scheduleManagement')}>
                    <EIcon type='calendar' sx={iconStyle}/>
                    <Typography>
                        {translator('labels.schedule-viewer')}
                    </Typography>
                </Button>

                <Button sx={btnStyle} onClick={() => navigate('/lessonsManagement')}>
                    <EIcon type='preview' sx={iconStyle}/>
                    <Typography>
                        {translator('labels.lessons-viewer')}
                    </Typography>
                </Button>

                <Button sx={btnStyle} onClick={() => navigate('/addUserToTenant')}>
                    <EIcon type='person add' sx={iconStyle}/>
                    <Box>
                        <Typography>
                            {translator('labels.add-user-to-tenant')}
                        </Typography>
                    </Box>
                </Button>

                <Button sx={btnStyle} onClick={() => navigate('/viewTenantRequests')}>
                    <EIcon type='list' sx={iconStyle}/>
                    <Box>
                        <Typography>
                            {translator('labels.view-tenant-requests')}
                        </Typography>
                    </Box>
                </Button>
            </Box>
        </PageBox>
);}