import { useLocation, useNavigate } from "react-router";
import { useStore } from "../../../api/stores/StoresManager";
import { cultureSelectStyle, headerBox, headerIconStyle, headerLeftSideBoxStyle, headerNavButtonStyle,
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
import EIcon from '../../wrappers/EIcon';
import { useRoleCheck } from "../../../hooks/useRoleCheck";
import { useTenantCheck } from "../../../hooks/useTenantCheck";
import pageRoutes from "../../../utils/RoutesProvider";
import { observer } from "mobx-react-lite";

const navOptions = { 
    replace: false
}

const Header = observer(() => {
    const { userStore, tenantStore } = useStore();
    const checkRole = useRoleCheck();
    const checkTenantNotEmpty = useTenantCheck();
    const navigate = useNavigate();
    const location = useLocation();

    const logout = () => {
        navigate(pageRoutes.logout, navOptions);
    }

    const userProfile = () => {
        navigate(pageRoutes.profile, navOptions);
    }

    const schedules = () => {
        navigate(`${pageRoutes.scheduleView}/teacher/${userStore.user?.id}`, navOptions);
    }

    const scheduleBuilder = () => {
        navigate(pageRoutes.managementPage, navOptions);
    }

    return( 
        <>
            {
                !location.pathname.startsWith(pageRoutes.confirmEmail) 
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
                            <Typography variant="h3" component="h3" sx={titleTextStyle}>
                                Eschedule
                            </Typography>
                            <Typography variant="h5" component="h5" sx={tenantTitleStyle}>
                                {tenantStore.tenant?.name}
                            </Typography>
                        </Box>
                    </Box>

                    {
                       !location.pathname.startsWith(pageRoutes.login) 
                    && !location.pathname.startsWith(pageRoutes.register) 
                    && !location.pathname.startsWith('/createTenant') 
                    &&
                        <Box sx={navigationButtonsStyle}>
                            <Avatar sx={[headerNavButtonStyle, profileNavButtonStyle]} onClick={userProfile}>
                                {userStore.user?.name[0].toUpperCase()}
                                {userStore.user?.lastName[0].toUpperCase()}
                            </Avatar>
                            {
                                (

                                    checkRole(Role.Dispatcher,
                                    () => checkTenantNotEmpty(
                                        () => <Button sx={headerNavButtonStyle} onClick={scheduleBuilder}>
                                                <EIcon sx={headerIconStyle} type='manage accounts' fontSize='large'/>
                                                </Button>
                                        )
                                    )
                                )
                            || 
                                (
                                    checkRole(Role.Teacher, 
                                    () => checkTenantNotEmpty(
                                        () => <Button sx={[headerNavButtonStyle]} onClick={schedules}>
                                                <EIcon sx={headerIconStyle} type='calendar' fontSize='large'/>
                                                </Button>
                                        )
                                    )
                                )
                            }

                            <Button sx={[headerNavButtonStyle]}
                                    onClick={logout}
                            >
                                <EIcon sx={headerIconStyle} type='quit' fontSize='large'/>
                            </Button>
                        </Box>
                    }
                </Box>
            }
        </>
    );
});

export default Header;