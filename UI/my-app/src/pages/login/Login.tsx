import { useNavigate } from "react-router-dom";
import { UserLoginModel } from "../../models/Users";
import { useStore } from "../../api/stores/StoresManager";
import { toast } from "react-toastify";
import { InputsBoxStyle, MainBoxStyle, RegisterButtonStyle } from "./LoginStyles";
import { useCult } from "../../hooks/useTranslator";
import { createTenantButtonStyle } from "../registration/RegistrationStyles";
import TextField from "@mui/material/TextField";
import Box from "@mui/material/Box";
import Button from "@mui/material/Button";
import Typography from "@mui/material/Typography";
import { useLoader } from "../../hooks/useLoader";
import Loader from "../../components/hoc/loading/Loader";
import PageBox from "../../components/wrappers/PageBox";
import { useInput } from "../../hooks/useInput";

export function LoginPage() {
    const navigate = useNavigate();
    const { translator } = useCult();
    const loader = useLoader();

    const emailInput = useInput('email');
    const passwordInput = useInput('text');

    const { userStore } = useStore();

    const hasErrors = () => {
        emailInput.ref.current?.focus();
        passwordInput.ref.current?.focus();
        passwordInput.ref.current?.blur();

        const isTouched = emailInput.value.length && passwordInput.value.length
        const hasAnyError = emailInput.errors.length || passwordInput.errors.length

        return !isTouched || (isTouched && hasAnyError)
    }

    const submit = async () => {
        if (hasErrors()) {
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
        navigate('/profile');
    };

    const redirectToRegistration = () => 
        navigate("/register");

    const redirectToTenantCreator = () => 
        navigate("/createTenant");

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
                        <TextField
                            label={translator('labels.email')}
                            variant="filled"
                            value={emailInput.value}
                            required={true}
                            helperText= {emailInput.errors}
                            error={emailInput.errors.length !== 0}
                            inputRef={emailInput.ref}
                            onFocus={emailInput.handleChange}
                            onChange={emailInput.handleChange}
                        />
                        <TextField
                            label={translator('labels.password')}
                            variant="filled"
                            type="password"
                            value={passwordInput.value}
                            required={true}
                            helperText= {passwordInput.errors}
                            error={passwordInput.errors.length !== 0}
                            inputRef={passwordInput.ref}
                            onFocus={passwordInput.handleChange}
                            onChange={passwordInput.handleChange}
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
