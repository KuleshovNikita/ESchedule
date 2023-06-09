import { useParams } from "react-router";
import { useStore } from "../../api/stores/StoresManager";
import { Box, Button, Typography } from "@mui/material";
import { useNavigate } from "react-router-dom";
import LoadingComponent from "../../components/hoc/loading/LoadingComponent";
import { useEffect, useState } from "react";
import { loginButtonStyle } from "../registration/RegistrationStyles";
import { useCult } from "../../hooks/Translator";

export default function ConfirmEmailPage() {
    const navigate = useNavigate();
    const { translator } = useCult();

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
                            {translator('messages.email-confirmed')}<br/>
                        </Typography>
                        <Typography>
                            {translator('messages.send-code-to-dispatcher')}
                        </Typography>
                        <Typography>
                            {userStore.user?.id}
                        </Typography>
                        <Button sx={loginButtonStyle} 
                                variant="contained" 
                                size="large" 
                                onClick={redirectToLogin}>
                            {translator('buttons.login')}
                        </Button>
                    </Box>
                : 
                    <LoadingComponent/>
            }            
        </Box>
    );
}