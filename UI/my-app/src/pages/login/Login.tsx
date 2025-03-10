import { useNavigate } from "react-router-dom";
import { UserLoginModel } from "../../models/Users";
import { useStore } from "../../api/stores/StoresManager";
import { toast } from "react-toastify";
import { InputsBoxStyle, MainBoxStyle, RegisterButtonStyle } from "./LoginStyles";
import { useCult } from "../../hooks/useTranslator";
import { createTenantButtonStyle } from "../registration/RegistrationStyles";
import Box from "@mui/material/Box";
import Button from "@mui/material/Button";
import Typography from "@mui/material/Typography";
import { useLoader } from "../../hooks/useLoader";
import Loader from "../../components/hoc/loading/Loader";
import PageBox from "../../components/wrappers/PageBox";
import { useInput } from "../../hooks/inputHooks/useInput";
import { ETextField } from "../../components/wrappers/ETextField";
import { useRenderTrigger } from "../../hooks/useRenderTrigger";
import { useInputValidator } from "../../hooks/inputHooks/useInputValidator";
import pageRoutes from "../../utils/RoutesProvider";

export function LoginPage() {
    const navigate = useNavigate();
    const { translator } = useCult();
    const loader = useLoader();

    const hasErrors = useInputValidator();
    const rerender = useRenderTrigger();

    const emailInput = useInput('email');
    const passwordInput = useInput('text');

    const { userStore } = useStore();

    const submit = async () => {        
        if (hasErrors(emailInput, passwordInput)) {
            rerender();
            return;
        }

        const user: UserLoginModel = { 
            login: emailInput.value, 
            password: passwordInput.value 
        };

        loader.show();

        await userStore.login(user)
            .then(() => toast.success(translator('toasts.welcome') + userStore.user?.name))
            .catch(err => loader.hide());

        loader.hide();
        navigate(pageRoutes.profile);
    };

    const redirectToRegistration = () => 
        navigate(pageRoutes.register);

    const redirectToTenantCreator = () => 
        navigate(pageRoutes.createTenant);

    return (
        <Loader>
            <PageBox>
                <Box
                    component="form"
                    sx={MainBoxStyle}
                    noValidate
                    autoComplete="off"
                >
                    <Box sx={InputsBoxStyle}>
                        <ETextField
                            label={translator('labels.email')}
                            inputProvider={emailInput}
                            required={true}
                        />
                        <ETextField
                            label={translator('labels.password')}
                            inputProvider={passwordInput}
                            required={true}
                            type="password"
                        />
                    </Box>
                    <Box sx={InputsBoxStyle}>
                        <Button variant="contained" size="large" onClick={submit}>
                            {translator('buttons.login')}
                        </Button>
                        <Typography>
                            {translator('words.or')}
                        </Typography>
                        <Button sx={RegisterButtonStyle} 
                                variant="contained" 
                                size="large" 
                                onClick={redirectToRegistration}>
                            {translator('buttons.register')}
                        </Button>
                        <Typography>
                        {translator('words.or')}
                        </Typography>
                        <Button sx={createTenantButtonStyle} 
                                variant="contained" 
                                size="large" 
                                onClick={redirectToTenantCreator}>
                            {translator('buttons.create-tenant')}
                        </Button>
                    </Box>
                </Box>
            </PageBox>
        </Loader>
    );
}
