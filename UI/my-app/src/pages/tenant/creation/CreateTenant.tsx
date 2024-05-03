import React, { useRef, useState } from "react";
import { useLocation } from "react-router";
import { useNavigate } from "react-router-dom";
import { useStore } from "../../../api/stores/StoresManager";
import { toast } from "react-toastify";
import { useCult } from "../../../hooks/Translator";
import { TenantCreateModel, TenantSettingsCreateModel } from "../../../models/Tenants";
import Box from "@mui/material/Box";
import TextField from "@mui/material/TextField";
import Button from "@mui/material/Button";
import Loader from "../../../components/hoc/loading/Loader";
import { useLoader } from "../../../hooks/Loader";
import { createTenantButtonStyle, formStyle, rowStyles } from "./TenantStyles";
import PageBox from "../../../components/wrappers/PageBox";

const EMAIL_REGEX = /^[\w-.]+@([\w-]+\.)+[\w-]{2,4}$/
type Focus = React.FocusEvent<HTMLInputElement | HTMLTextAreaElement, Element>; 

const hrStyle = {
    width: "210px",
    marginTop: "20px",
    marginBottom: "0px"
}

export function CreateTenant() {

    const navigate = useNavigate();
    const { translator } = useCult();
    const loader = useLoader();

    const [tenantName, setTenantName] = useState("");
    const [tenantErrors, setTenantErrors] = useState("");
    const [studyDayStartTime, setStudyDayStartTime] = useState("");
    const [studyDayStartTimeErrors, setStudyDayStartTimeErrors] = useState("");
    const [lessonDurationTime, setLessonDurationTime] = useState("");
    const [lessonDurationTimeErrors, setLessonDurationTimeErrors] = useState("");
    const [breaksDurationTime, setBreaksDurationTime] = useState("");
    const [breaksDurationTimeErrors, setBreaksDurationTimeErrors] = useState("");
    const [email, setEmail] = useState("");
    const [emailErrors, setEmailErrors] = useState("");
    const [password, setPassword] = useState("");
    const [passwordErrors, setPasswordErrors] = useState("");

    const { tenantStore } = useStore();

    const tenantRef = useRef<HTMLInputElement>();
    const studyDayStartTimeRef = useRef<HTMLInputElement>();
    const lessonDurationTimeRef = useRef<HTMLInputElement>();
    const breaksDurationTimeRef = useRef<HTMLInputElement>();
    const emailRef = useRef<HTMLInputElement>();
    const passwordRef = useRef<HTMLInputElement>();

    const handleTenantNameChange = (e: Focus) => {
        const tenant = e.target.value;

        if(tenant.length === 0) {
            setTenantErrors(translator('input-helpers.tenant-name-required'));
        } else {
            setTenantErrors('');
        }

        setTenantName(tenant);
    }

    const handleStudyDayStartTimeChange = (e: Focus) => {
        const time = e.target.value;

        if(time.length === 0) {
            setStudyDayStartTimeErrors(translator('input-helpers.study-start-time-required'));
        } else {
            setStudyDayStartTimeErrors('');
        }

        setStudyDayStartTime(time);
    }

    const handleLessonDurationTimeChange = (e: Focus) => {
        const time = e.target.value;

        if(time.length === 0) {
            setLessonDurationTimeErrors(translator('input-helpers.lesson-duration-time-required'));
        } else {
            setLessonDurationTimeErrors('');
        }

        setLessonDurationTime(time);
    }

    const handleBreaksDurationTimeChange = (e: Focus) => {
        const time = e.target.value;

        if(time.length === 0) {
            setBreaksDurationTimeErrors(translator('input-helpers.breaks-duration-time-required'));
        } else {
            setBreaksDurationTimeErrors('');
        }

        setBreaksDurationTime(time);
    }

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
        tenantRef.current?.focus();
        emailRef.current?.focus();
        studyDayStartTimeRef.current?.focus();
        lessonDurationTimeRef.current?.focus();
        breaksDurationTimeRef.current?.focus();
        passwordRef.current?.focus();
        passwordRef.current?.blur();

        const isTouched = email.length 
                        && password.length 
                        && tenantName.length 
                        && studyDayStartTime.length 
                        && lessonDurationTime.length 
                        && breaksDurationTime.length 
        const hasAnyError = emailErrors.length 
                        || passwordErrors.length 
                        || tenantErrors.length 
                        || studyDayStartTimeErrors.length
                        || lessonDurationTimeErrors.length
                        || breaksDurationTimeErrors.length

        return !isTouched || (isTouched && hasAnyError)
    }

    const submit = async () => {
        if (hasErrors()) {
            return;
        }

        const settings: TenantSettingsCreateModel = {
            studyDayStartTime: studyDayStartTime,
            lessonDurationTime: lessonDurationTime,
            breaksDurationTime: breaksDurationTime,
        }
        const tenant: TenantCreateModel = { 
            name: tenantName,
            login: email, 
            password: password,
            settings: settings
        };

        loader.show();

        await tenantStore.createTenant(tenant)
            .then(() => toast.success(translator('toasts.tenant-created')))
            .then(() => loader.hide())
            .then(() => navigate('/login'))
            .catch(err => loader.hide());
    };

    return (
        <Loader>
            <PageBox>
                <Box
                    component="form"
                    sx={formStyle}
                    noValidate
                    autoComplete="off"
                >    
                    <Box>
                        <Box sx={rowStyles}>              
                            <TextField
                                label={translator('labels.tenant-name')}
                                variant="filled"
                                value={tenantName}
                                required={true}
                                helperText= {tenantErrors}
                                error={tenantErrors.length !== 0}
                                inputRef={tenantRef}
                                onFocus={(e: Focus) => handleTenantNameChange(e)}
                                onChange={handleTenantNameChange}
                            />
                        </Box>
                        <Box sx={rowStyles}>  
                            <TextField
                                label={translator('labels.study-day-start-time')}
                                variant="filled"
                                type="time"
                                value={studyDayStartTime}
                                required={true}
                                helperText= {studyDayStartTimeErrors}
                                error={studyDayStartTimeErrors.length !== 0}
                                inputRef={studyDayStartTimeRef}
                                onFocus={(e: Focus) => handleStudyDayStartTimeChange(e)}
                                onChange={handleStudyDayStartTimeChange}
                            />
                            <TextField
                                label={translator('labels.lesson-duration-time')}
                                variant="filled"
                                type='time'
                                value={lessonDurationTime}
                                required={true}
                                helperText= {lessonDurationTimeErrors}
                                error={lessonDurationTimeErrors.length !== 0}
                                inputRef={lessonDurationTimeRef}
                                onFocus={(e: Focus) => handleLessonDurationTimeChange(e)}
                                onChange={handleLessonDurationTimeChange}
                            />
                            <TextField
                                label={translator('labels.breaks-duration-time')}
                                variant="filled"
                                type="time"
                                value={breaksDurationTime}
                                required={true}
                                helperText= {breaksDurationTimeErrors}
                                error={breaksDurationTimeErrors.length !== 0}
                                inputRef={breaksDurationTimeRef}
                                onFocus={(e: Focus) => handleBreaksDurationTimeChange(e)}
                                onChange={handleBreaksDurationTimeChange}
                            />
                        </Box>  
                        <Box sx={rowStyles}>
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
                    </Box>
                    <Box>
                        <Button sx={createTenantButtonStyle} 
                            variant="contained" 
                            size="large" 
                            onClick={submit}>
                                {translator('buttons.create-tenant')}
                        </Button>
                    </Box>
                </Box>
            </PageBox>
        </Loader>
    );
}