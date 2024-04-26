import { Box, Button, TextField, Typography } from "@mui/material";
import React, { useRef, useState } from "react";
import { useLocation } from "react-router";
import { useNavigate, Link as RouterLink } from "react-router-dom";
import { UserLoginModel } from "../../models/Users";
import { useStore } from "../../api/stores/StoresManager";
import { toast } from "react-toastify";
import { InputFormStyle, RegisterButtonStyle } from "./LoginStyles";
import LoadingComponent from "../../components/hoc/loading/LoadingComponent";
import { useCult } from "../../hooks/Translator";

const EMAIL_REGEX = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/
type Focus = React.FocusEvent<HTMLInputElement | HTMLTextAreaElement, Element>; 

export function LoginPage() {
    const navigate = useNavigate();
    const location = useLocation();
    const { translator } = useCult();

    const fromPage = location.state?.form?.pathname || "/";

    const [isWaitingForAuth, setAuthWaiter] = useState(false);

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

        setAuthWaiter(true);
        const isSuccessful = await userStore.login(user);

        if(isSuccessful) {
            toast.success(translator('toasts.welcome') + userStore.user?.name);
        } 

        navigate(fromPage, { replace: true });
    };

    const redirectToRegistration = () => {
        navigate("/register", { replace: true });
    }

    return (
        <>
            {
                isWaitingForAuth === true
            ?
                <LoadingComponent/>
            :
            <>
                <Box
                    component="form"
                    sx={InputFormStyle}
                    noValidate
                    autoComplete="off"
                >
                    <TextField
                        label={translator('labels.email')}
                        variant="filled"
                        value={email}
                        required={true}
                        helperText= {emailErrors}
                        error={emailErrors.length !== 0}
                        inputRef={emailRef}
                        onFocus={(e) => handleEmailChange(e)}
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
                        onFocus={(e) => handlePasswordChange(e)}
                        onChange={handlePasswordChange}
                    />
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
                </Box>
            </>
            }
        </>
    );
}
