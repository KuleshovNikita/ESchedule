import { useParams } from "react-router";
import { useStore } from "../../api/stores/StoresManager";
import { Box, Button, Typography } from "@mui/material";
import { useNavigate } from "react-router-dom";
import LoadingComponent from "../../components/hoc/loading/LoadingComponent";
import { useEffect, useState } from "react";
import { loginButtonStyle } from "../registration/RegistrationStyles";

export default function ConfirmEmail() {
    const navigate = useNavigate();

    const [finished, setFinishState] = useState(false);
    const { userStore } = useStore();
    const { key } = useParams();

    useEffect(() => { 
        sendEmailConfirmation();
    }, []);

    const sendEmailConfirmation = async () => {
        if(!key) {
            navigate('/notFound', { replace: true })
        }

        await userStore.confirmEmail(encodeURIComponent(key as string));
        setFinishState(true);
    }

    const redirectToLogin = () => {
        navigate("/login", { replace: true });
    }

    return (
        <Box>
            {
                    finished
                ? 
                    <Box sx={{textAlign: 'center'}}>
                        <Typography variant="h2">
                            Email is successfully confirmed
                        </Typography>
                        <Button sx={loginButtonStyle} 
                                variant="contained" 
                                size="large" 
                                onClick={redirectToLogin}>
                            Login
                        </Button>
                    </Box>
                : 
                    <LoadingComponent/>
            }            
        </Box>
    );
}