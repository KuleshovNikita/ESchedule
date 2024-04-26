import { useParams } from "react-router";
import { useStore } from "../../api/stores/StoresManager";
import { Box, Button, Typography } from "@mui/material";
import { useNavigate } from "react-router-dom";
import LoadingComponent from "../../components/hoc/loading/LoadingComponent";
import { useEffect, useState } from "react";
import { loginButtonStyle } from "../registration/RegistrationStyles";
import { useCult } from "../../hooks/Translator";
import { toast } from "react-toastify";

export default function ConfirmEmailPage() {
    const navigate = useNavigate();
    const { translator } = useCult();

    const [finished, setFinishState] = useState(false);
    const { userStore } = useStore();
    const [ userId, setUserId ] = useState('');
    const { key } = useParams();

    useEffect(() => { 
        sendEmailConfirmation();
    }, []);

    const sendEmailConfirmation = async () => {
        if(!key) {
            navigate('/notFound', { replace: true })
        }

        const response = await userStore.confirmEmail(encodeURIComponent(key as string));
        if(response.isSuccessful) {
            setUserId(response.value);
        } else {
            toast.error(response.clientErrorMessage);
        }
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
                            {userId}
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