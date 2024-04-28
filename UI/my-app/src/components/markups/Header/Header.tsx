import { Avatar, Box, Button, MenuItem, Select, Typography } from "@mui/material";
import LogoutIcon from '@mui/icons-material/Logout';
import { useLocation, useNavigate } from "react-router";
import { useStore } from "../../../api/stores/StoresManager";
import CalendarMonthIcon from '@mui/icons-material/CalendarMonth';
import ManageAccountsIcon from '@mui/icons-material/ManageAccounts';
import { cultureSelectStyle, headerBox, headerLeftSideBoxStyle, headerNavButtonStyle,
         navigationButtonsStyle, 
         profileNavButtonStyle, 
         titleTextStyle } from "./HeaderStyles";
import { Role } from "../../../models/Users";
import { getDefLang, locales, updateLang } from "../../../translations/Localization";

const navOptions = { 
    replace: false
}

export default function Header() {
    const { userStore } = useStore();
    const navigate = useNavigate();
    const location = useLocation();

    const logout = () => {
        navigate('/logout', navOptions);
    }

    const userProfile = () => {
        navigate('/profile', navOptions);
    }

    const schedules = () => {
        navigate(`/schedule/${true}/${userStore.user?.id}`, navOptions);
    }

    const scheduleBuilder = () => {
        navigate('/scheduleBuilder', navOptions);
    }

    return( 
        <>
            {
                !location.pathname.startsWith('/confirmEmail') 
             &&
                <Box sx={headerBox}>
                    <Box sx={headerLeftSideBoxStyle}>
                        <Typography variant="h2" component="h2" sx={titleTextStyle}>
                            Eschedule
                        </Typography>
                        <Select defaultValue={getDefLang} sx={cultureSelectStyle}>
                        {
                            Object.keys(locales).map((key) => {
                                return  <MenuItem value={key} 
                                                  key={key} 
                                                  onClick={() => updateLang(key)}>
                                            {locales[key as keyof typeof locales]}
                                        </MenuItem>
                            })
                        }
                        </Select>
                    </Box>

                    {
                       !location.pathname.startsWith('/login') 
                    && !location.pathname.startsWith('/register') 
                    && !location.pathname.startsWith('/createTenant') 
                    &&
                        <Box sx={navigationButtonsStyle}>
                            <Avatar sx={[headerNavButtonStyle, profileNavButtonStyle]} onClick={userProfile}>
                                {userStore.user?.name[0].toUpperCase()}
                                {userStore.user?.lastName[0].toUpperCase()}
                            </Avatar>

                            {
                                (
                                    userStore.user?.role === Role.Dispatcher
                                &&
                                    <Button sx={[headerNavButtonStyle]}
                                            onClick={scheduleBuilder}
                                    >
                                        <ManageAccountsIcon fontSize="large"/>
                                    </Button>
                                )
                            || 
                                (
                                    userStore.user?.role === Role.Teacher
                                &&
                                    <Button sx={[headerNavButtonStyle]}
                                            onClick={schedules}
                                    >
                                        <CalendarMonthIcon fontSize="large"/>
                                    </Button>
                                )
                            }

                            <Button sx={[headerNavButtonStyle]}
                                    onClick={logout}
                            >
                                <LogoutIcon fontSize="large"/>
                            </Button>
                        </Box>
                    }
                </Box>
            }
        </>
    );
}