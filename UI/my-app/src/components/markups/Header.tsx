import { Avatar, Box, Button, Typography } from "@mui/material";
import LogoutIcon from '@mui/icons-material/Logout';
import { useLocation, useNavigate } from "react-router";
import { useStore } from "../../api/stores/StoresManager";

const headerBox = {
    bgcolor: "orange",
    mt: -1,
    ml: -1, 
    mr: -1,
    display: "grid",
    gridTemplateColumns: "6fr 1fr"
}

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

    return( 
        <>
            {
                !location.pathname.startsWith('/login') 
             &&
                <Box sx={headerBox}>
                    <Typography  variant="h2" component="h2" sx={{ display: "inline-block"}}>
                        Eschedule
                    </Typography>
                        <Box sx={{ display: "flex", justifyContent: "center", mt: 2}}>
                        <Avatar /*sx={listItemButton}*/ onClick={userProfile}>
                            {userStore.user?.name[0].toUpperCase()}
                            {userStore.user?.lastName[0].toUpperCase()}
                        </Avatar>
                        <Button sx={{/*...listItemButton,*/ width: "20px", height: "40px"}}
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