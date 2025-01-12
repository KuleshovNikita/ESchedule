import React, { useRef, useState } from "react";
import { useLocation } from "react-router";
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

const EMAIL_REGEX = /^[\w-.]+@([\w-]+\.)+[\w-]{2,4}$/
type Focus = React.FocusEvent<HTMLInputElement | HTMLTextAreaElement, Element>; 

export function LoginPage() {
    const navigate = useNavigate();
    const location = useLocation();
    const { translator } = useCult();
    const loader = useLoader();

    const [email, setEmail] = useState("");
    const [emailErrors, setEmailErrors] = useState("");
    const [password, setPassword] = useState("");
    const [passwordErrors, setPasswordErrors] = useState("");

    const { userStore } = useStore();

    const emailRef = useRef<HTMLInputElement>();
    const passwordRef = useRef<HTMLInputElement>();

    const handleEmailChange = (e: Focus) => {
        const email = e.target.value;

        if(email.length === 0) {
            setEmailErrors(translator('input-helpers.email-required'));
        } else if(!email.match(EMAIL_REGEX)) {
            setEmailErrors(translator('input-helpers.email-should-be-correct'));
        } else {
            setEmailErrors('');
        }

        setEmail(email);
    }

    const handlePasswordChange = (e: Focus) => {
        const password = e.target.value;

        if (password.length === 0) {
            setPasswordErrors(translator('input-helpers.please-enter-password'));
        } else {
            setPasswordErrors('');
        }

        setPassword(password);
    }

    const hasErrors = () => {
        emailRef.current?.focus();
        passwordRef.current?.focus();
        passwordRef.current?.blur();

        const isTouched = email.length && password.length
        const hasAnyError = emailErrors.length || passwordErrors.length

        return !isTouched || (isTouched && hasAnyError)
    }

    const submit = async () => {
        if (hasErrors()) {
            return;
        }

        const user: UserLoginModel = { 
            login: email, 
            password: password 
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
                            value={email}
                            required={true}
                            helperText= {emailErrors}
                            error={emailErrors.length !== 0}
                            inputRef={emailRef}
                            onFocus={(e: Focus) => handleEmailChange(e)}
                            onChange={handleEmailChange}
                        />
                        <TextField
                            label={translator('labels.password')}
                            variant="filled"
                            type="password"
                            value={password}
                            required={true}
                            helperText= {passwordErrors}
                            error={passwordErrors.length !== 0}
                            inputRef={passwordRef}
                            onFocus={(e: Focus) => handlePasswordChange(e)}
                            onChange={handlePasswordChange}
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
