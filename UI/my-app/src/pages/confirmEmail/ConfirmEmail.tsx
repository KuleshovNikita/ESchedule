import { useParams } from "react-router";
import { useStore } from "../../api/stores/StoresManager";
import { useNavigate } from "react-router-dom";
import Loader from "../../components/hoc/loading/Loader";
import { useEffect, useState } from "react";
import { loginButtonStyle } from "../registration/RegistrationStyles";
import { useCult } from "../../hooks/useTranslator";
import Typography from "@mui/material/Typography";
import Box from "@mui/material/Box";
import Button from "@mui/material/Button";
import { useLoader } from "../../hooks/useLoader";
import PageBox from "../../components/wrappers/PageBox";

export default function ConfirmEmailPage() {
    const navigate = useNavigate();
    const { translator } = useCult();
    const { userStore } = useStore();
    const [ userId, setUserId ] = useState('');
    const { key } = useParams();
    const loader = useLoader();

    useEffect(() => { 
        sendEmailConfirmation();
    }, []);

    const sendEmailConfirmation = async () => {
        if(!key) {
            navigate('/notFound', { replace: true })
        }

        loader.show();

        const response = await userStore.confirmEmail(encodeURIComponent(key as string));
        if(response) {
            setUserId(response);
        } 
        
        loader.hide();
    }

    const redirectToLogin = () => {
        navigate("/login", { replace: true });
    }

    return (
        <Loader replace>
            <PageBox>
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
            </PageBox>
        </Loader> 
    );
}