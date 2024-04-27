import { Box, Button, FormControl, InputLabel, MenuItem, Select, TextField, Typography } from "@mui/material";
import { useEffect, useRef, useState } from "react";
import { useNavigate } from "react-router-dom";
import { useStore } from "../../api/stores/StoresManager";
import { toast } from 'react-toastify';
import { loginButtonStyle, registrationFormStyle } from "./RegistrationStyles";
import { Role, UserCreateModel } from "../../models/Users";
import { useCult } from "../../hooks/Translator";

const EMAIL_REGEX = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/

export default function RegistrationPage() {
    const { translator } = useCult();
    const [name, setName] = useState('');
    const [nameErrors, setNameErrors] = useState('');

    const [lastName, setLastName] = useState('');
    const [lastNameErrors, setLastNameErrors] = useState('');

    const [fatherName, setFatherName] = useState('');
    const [fatherNameErrors, setFatherNameErrors] = useState('');

    const [age, setAge] = useState('');
    const [ageErrors, setAgeErrors] = useState('');

    const [role, setRole] = useState(Role.Pupil);

    const [email, setEmail] = useState('');
    const [emailErrors, setEmailErrors] = useState("");

    const [password, setPassword] = useState('');
    const [passwordErrors, setPasswordErrors] = useState("");

    const [repeatPassword, setRepeatPassword] = useState('');
    const [repeatPasswordErrors, setRepeatPasswordErrors] = useState('');

    const { userStore } = useStore();
    const navigate = useNavigate();

    const firstRender = useRef(true);

    useEffect(() => {
        console.log('effect');
        if (firstRender.current) {
            firstRender.current = false;
            return;
        }

        validateForm();
    }, [name, lastName, fatherName, age, role, email, password, repeatPassword]);

    const validateForm = () => {
        validateFirstName();
        validateLastName();
        validateFatherName();
        validateAge();
        validateEmail();
        validatePassword();
        validateRepeatPassword();
    }

    const validateFirstName = () => {
        if (name.length === 0) {
            setNameErrors(translator('input-helpers.first-name-required'));
        } else {
            setNameErrors('');
        }
    }

    const validateLastName = () => {
        if (lastName.length === 0) {
            setLastNameErrors(translator('input-helpers.last-name-required'));
        } else {
            setLastNameErrors('');
        }
    }

    const validateFatherName = () => {
        if (fatherName.length === 0) {
            setFatherNameErrors(translator('input-helpers.father-name-required'));
        } else {
            setFatherNameErrors('');
        }
    }

    const validateAge = () => {
        const num = Number(age);

        if (!num || num < 5) {
            setAgeErrors(translator('input-helpers.minimal-age-5'));
        } else if (num > 99) {
            setAgeErrors(translator('input-helpers.maximal-age-99'));
        } else {
            setAgeErrors('');
        }
    }

    const validateEmail = () => {
        if (email.length === 0) {
            setEmailErrors(translator('input-helpers.email-required'));
        } else if (!email.match(EMAIL_REGEX)) {
            setEmailErrors(translator('input-helpers.email-should-be-correct'));
        } else {
            setEmailErrors('');
        }
    }

    const validatePassword = () => {
        if (password.length === 0) {
            setPasswordErrors(translator('input-helpers.please-enter-password'));
        } else {
            setPasswordErrors('');
        }
    }

    const validateRepeatPassword = () => {
        if (repeatPassword.length === 0) {
            setRepeatPasswordErrors(translator('input-helpers.please-repeat-password'));
        } else if (repeatPassword !== password) {
            setRepeatPasswordErrors(translator('input-helpers.passwords-do-not-match'));
        } else {
            setRepeatPasswordErrors('');
        }
    }

    const hasErrors = () => 
        nameErrors.length 
        || lastNameErrors.length 
        || fatherNameErrors.length 
        || ageErrors.length 
        || emailErrors.length 
        || passwordErrors.length 
        || repeatPasswordErrors.length;

    const submit = async () => {
        if (hasErrors()) {
            return;
        }

        const user: UserCreateModel = {
            login: email, 
            name: name,
            lastName: lastName, 
            fatherName: fatherName, 
            age: Number(age),
            password: password,
            role: role as Role,
            tenantId: '00000000-0000-0000-0000-000000000001'
        };

        await userStore.register(user);
        
        navigate("/login");
        toast.success(translator('toasts.verification-email-sent'));
    }

    const redirectToLogin = () => {
        navigate("/login", { replace: true });
    }

    const getRolesItems = () => {
        const values = Object.values(Role).filter(x => typeof x !== 'string') as Role[];
        const result = values.map((vk, key) => <MenuItem key={key} value={vk}>
                                                    {translator('roles.' + Role[vk])}
                                                </MenuItem>
                                );

        return result;
    }

    return (
        <>
            <Box
                component="form"
                sx={registrationFormStyle}
                autoComplete="off"
            >
                <TextField
                    label={translator('labels.first-name')}
                    variant="filled"
                    value={name}
                    required={true}
                    helperText={nameErrors}
                    error={nameErrors.length !== 0}
                    onChange={e => setName(e.target.value)}
                />
                <TextField
                    label={translator('labels.last-name')}
                    variant="filled"
                    value={lastName}
                    required={true}
                    helperText={lastNameErrors}
                    error={lastNameErrors.length !== 0}
                    onChange={e => setLastName(e.target.value)}
                />
                <TextField 
                    label={translator('labels.father-name')}
                    variant="filled"
                    size="small"
                    helperText={fatherNameErrors}
                    value={fatherName}
                    required={true}
                    error={fatherNameErrors.length !== 0}
                    margin="dense"
                    onChange={e => setFatherName(e.target.value)}
                />
                <TextField
                    label={translator('labels.age')}
                    variant="filled"
                    value={age}
                    type="number"
                    required
                    helperText={ageErrors}
                    error={ageErrors.length !== 0}
                    margin="dense"
                    onChange={e => setAge(e.target.value)}
                />
                <FormControl>
                    <InputLabel id="role-registration-select-label">{translator('labels.role')}</InputLabel>
                    <Select
                        label={translator('labels.role')}
                        id="role-registration-select"
                        labelId="role-registration-select-label"
                        variant="filled"
                        required
                        value={role}
                        onChange={e => setRole(e.target.value as Role)}
                    >
                        { getRolesItems() }
                    </Select>
                </FormControl>
                <TextField
                    label={translator('labels.email')}
                    variant="filled"
                    value={email}
                    required={true}
                    helperText={emailErrors}
                    error={emailErrors.length !== 0}
                    onChange={e => setEmail(e.target.value)}
                />
                <TextField
                    label={translator('labels.password')}
                    variant="filled"
                    type="password"
                    value={password}
                    required={true}
                    helperText={passwordErrors}
                    error={passwordErrors.length !== 0}
                    onChange={e => setPassword(e.target.value)}
                />
                <TextField
                    label={translator('labels.repeat-password')}
                    variant="filled"
                    type="password"
                    value={repeatPassword}
                    required={true}
                    helperText={repeatPasswordErrors}
                    error={repeatPasswordErrors.length !== 0}
                    onChange={e => setRepeatPassword(e.target.value)}
                />
                
                <Button variant="contained" size="large" onClick={submit}>
                    {translator('buttons.register')}
                </Button>
                <Typography>
                    {translator('words.or')}
                </Typography>
                <Button sx={loginButtonStyle} 
                        variant="contained" 
                        size="large" 
                        onClick={redirectToLogin}>
                    {translator('buttons.login')}
                </Button>
            </Box>
        </>
    )
}

