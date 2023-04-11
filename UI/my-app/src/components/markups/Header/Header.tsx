import { Avatar, Box, Button, Typography } from "@mui/material";
import LogoutIcon from '@mui/icons-material/Logout';
import { useLocation, useNavigate } from "react-router";
import { useStore } from "../../../api/stores/StoresManager";
import CalendarMonthIcon from '@mui/icons-material/CalendarMonth';
import { headerBox, headerNavButtonStyle,
         navigationButtonsStyle, 
         profileNavButtonStyle, 
         titleTextStyle } from "./HeaderStyles";

export default function Header() {
    const { userStore } = useStore();
    const navigate = useNavigate();
    const location = useLocation();

    const logout = () => {
        navigate('/logout', { replace: true });
    }

    const userProfile = () => {
        navigate('/profile', { replace: true });
    }

    const schedules = () => {
        navigate('/schedule', { replace: true });
    }

    return( 
        <>
            {
                !location.pathname.startsWith('/login') 
             && !location.pathname.startsWith('/register') 
             && !location.pathname.startsWith('/confirmEmail') 
             &&
                <Box sx={headerBox}>
                    <Typography variant="h2" component="h2" sx={titleTextStyle}>
                        Eschedule
                    </Typography>
                    <Box sx={navigationButtonsStyle}>
                        <Avatar sx={[headerNavButtonStyle, profileNavButtonStyle]} onClick={userProfile}>
                            {userStore.user?.name[0].toUpperCase()}
                            {userStore.user?.lastName[0].toUpperCase()}
                        </Avatar>

                        <Button sx={[headerNavButtonStyle]}
                                onClick={schedules}
                        >
                            <CalendarMonthIcon fontSize="large"/>
                        </Button>

                        <Button sx={[headerNavButtonStyle]}
                                onClick={logout}
                        >
                            <LogoutIcon fontSize="large"/>
                        </Button>
                    </Box>
                </Box>
            }
        </>
    );
}