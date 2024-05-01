import { useLocation, useNavigate } from "react-router";
import { useStore } from "../../../api/stores/StoresManager";
import { cultureSelectStyle, headerBox, headerLeftSideBoxStyle, headerNavButtonStyle,
         labelStyles,
         navigationButtonsStyle, 
         profileNavButtonStyle, 
         tenantTitleStyle, 
         titleTextStyle } from "./HeaderStyles";
import { Role } from "../../../models/Users";
import { getDefLang, locales, updateLang } from "../../../translations/Localization";
import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';
import Select from '@mui/material/Select';
import MenuItem from '@mui/material/MenuItem';
import Avatar from '@mui/material/Avatar';
import Button from '@mui/material/Button';
import Icon from '../../wrappers/Icon';

const navOptions = { 
    replace: false
}

const Header = () => {
    const { userStore, tenantStore } = useStore();
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
                        <Box sx={labelStyles}>
                            <Typography variant="h3" component="h2" sx={titleTextStyle}>
                                Eschedule
                            </Typography>
                            <Typography variant="h5" component="h2" sx={tenantTitleStyle}>
                                {tenantStore.tenant?.name}
                            </Typography>
                        </Box>
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
                                    <Button sx={headerNavButtonStyle}
                                            onClick={scheduleBuilder}
                                    >
                                        <Icon type='manage accounts' fontSize='large'/>
                                    </Button>
                                )
                            || 
                                (
                                    userStore.user?.role === Role.Teacher
                                &&
                                    <Button sx={[headerNavButtonStyle]}
                                            onClick={schedules}
                                    >
                                        <Icon type='calendar' fontSize='large'/>
                                    </Button>
                                )
                            }

                            <Button sx={[headerNavButtonStyle]}
                                    onClick={logout}
                            >
                                <Icon type='quit' fontSize='large'/>
                            </Button>
                        </Box>
                    }
                </Box>
            }
        </>
    );
}

export default Header;