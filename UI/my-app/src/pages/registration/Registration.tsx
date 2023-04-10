import { Box, Button, FormControl, FormHelperText, InputLabel, MenuItem, Select, SelectChangeEvent, TextField, Typography } from "@mui/material";
import { useRef, useState } from "react";
import { useNavigate } from "react-router-dom";
import { useStore } from "../../api/stores/StoresManager";
import { toast } from 'react-toastify';
import { loginButtonStyle, registrationFormStyle } from "./RegistrationStyles";
import { Role, UserCreateModel } from "../../models/Users";

const EMAIL_REGEX = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/
type Focus = React.FocusEvent<HTMLInputElement | HTMLTextAreaElement, Element>;

export default function Registration() {
    const [name, setName] = useState('');
    const [nameErrors, setNameErrors] = useState('');

    const [lastName, setLastName] = useState('');
    const [lastNameErrors, setLastNameErrors] = useState('');

    const [fatherName, setFatherName] = useState('');
    const [fatherNameErrors, setFatherNameErrors] = useState('');

    const [age, setAge] = useState(5);
    const [ageErrors, setAgeErrors] = useState('');

    const [role, setRole] = useState(Role.Pupil);
    const [roleErrors, setRoleErrors] = useState("");

    const [email, setEmail] = useState('');
    const [emailErrors, setEmailErrors] = useState("");

    const [password, setPassword] = useState('');
    const [passwordErrors, setPasswordErrors] = useState("");

    const [repeatPassword, setRepeatPassword] = useState('');
    const [repeatPasswordErrors, setRepeatPasswordErrors] = useState('');

    const nameRef = useRef<HTMLInputElement>();
    const lastNameRef = useRef<HTMLInputElement>();
    const fatherNameRef = useRef<HTMLInputElement>();
    const ageRef = useRef<HTMLInputElement>();
    const roleRef = useRef<HTMLInputElement>();
    const emailRef = useRef<HTMLInputElement>();
    const passwordRef = useRef<HTMLInputElement>();
    const repeatPasswordRef = useRef<HTMLInputElement>();

    const { userStore } = useStore();
    const navigate = useNavigate();

    const handleFirstNameChange = (e: Focus) => {
        const firstName = e.target.value;

        if (firstName.length === 0) {
            setNameErrors('First name is required');
        } else {
            setNameErrors('');
        }

        setName(firstName);
    }

    const handleLastNameChange = (e: Focus) => {
        const lastName = e.target.value;

        if (lastName.length === 0) {
            setLastNameErrors('Last name is required');
        } else {
            setLastNameErrors('');
        }

        setLastName(lastName);
    }

    const handleFatherNameChange = (e: Focus) => {
        const fatherName = e.target.value;

        if (fatherName.length === 0) {
            setFatherNameErrors('Father name is required');
        } else {
            setFatherNameErrors('');
        }

        setFatherName(fatherName);
    }

    const handleAgeChange = (e: Focus) => {
        const age = Number(e.target.value);

        if(!age) {
            return;
        } else if (age < 5) {
            setAgeErrors('The minimal allowed age is 5');
        } else if (age > 99) {
            setAgeErrors('The maximal allowed age is 99');
        } else {
            setAgeErrors('');
        }

        setAge(age);
    }

    const handleRoleChange = (e: SelectChangeEvent<number>) => {
        const role = e.target.value;
        setRole(role as Role);
    }

    const handleEmailChange = (e: Focus) => {
        const email = e.target.value;

        if (email.length === 0) {
            setEmailErrors('Email is required');
        } else if (!email.match(EMAIL_REGEX)) {
            setEmailErrors('Email should be in correct format');
        } else {
            setEmailErrors('');
        }

        setEmail(email);
    }

    const handlePasswordChange = (e: Focus) => {
        const password = e.target.value;

        if (password.length === 0) {
            setPasswordErrors('Please enter password');
        } else {
            setPasswordErrors('');
        }

        setPassword(password);
    }

    const handleRepeatPasswordChange = (e: Focus) => {
        const repeatPassword = e.target.value;

        if (repeatPassword.length === 0) {
            setRepeatPasswordErrors('Please enter repeat password');
        } else if (repeatPassword !== password) {
            setRepeatPasswordErrors('Passwords don\'t match');
        } else {
            setRepeatPasswordErrors('');
        }

        setRepeatPassword(repeatPassword);
    }

    const hasErrors = () => {
        nameRef.current?.focus();
        lastNameRef.current?.focus();
        fatherNameRef.current?.focus();
        ageRef.current?.focus();
        emailRef.current?.focus();
        passwordRef.current?.focus();
        repeatPasswordRef.current?.focus();

        const isTouched =  name.length 
                        && lastName.length
                        && fatherName.length
                        && age
                        && email.length
                        && password.length
                        && repeatPassword.length;

        const hasAnyErrors = nameErrors.length 
                         || lastNameErrors.length 
                         || fatherNameErrors.length 
                         || ageErrors.length 
                         || roleErrors.length 
                         || emailErrors.length 
                         || passwordErrors.length 
                         || repeatPasswordErrors.length;

        return !isTouched || (isTouched && hasAnyErrors);
    }

    const submit = async () => {
        if (hasErrors()) {
            return;
        }

        const user: UserCreateModel = {
            login: email, 
            name: name,
            lastName: lastName, 
            fatherName: fatherName, 
            age: age,
            password: password,
            role: role as Role,
            tenantId: '00000000-0000-0000-0000-000000000001'
        };

        const result = await userStore.register(user);
        
        if(!result.isSuccessful) {
            navigate("/login");
            toast.success('We have sent a verification list to your email, please, follow the instructions there');
        } 
    }

    const redirectToLogin = () => {
        navigate("/login", { replace: true });
    }

    const getRolesItems = () => {
        const values = Object.values(Role).filter(x => typeof x !== 'string') as Role[];
        const result = values.map((vk, key) => <MenuItem key={key} value={vk}>
                                            {Role[vk]}
                                        </MenuItem>
                                );

        return result;
    }

    return (
        <>
            <Typography variant="h1" 
                        component="h1"
                        align="center">
                ESchedule
            </Typography>
            <Box
                component="form"
                sx={registrationFormStyle}
                autoComplete="off"
            >
                <TextField
                    label="First Name"
                    variant="filled"
                    value={name}
                    required={true}
                    helperText={nameErrors}
                    error={nameErrors.length !== 0}
                    inputRef={nameRef}
                    onFocus={(e) => handleFirstNameChange(e)}
                    onChange={handleFirstNameChange}
                />
                <TextField
                    label="Last Name"
                    variant="filled"
                    value={lastName}
                    required={true}
                    helperText={lastNameErrors}
                    error={lastNameErrors.length !== 0}
                    inputRef={lastNameRef}
                    onFocus={(e) => handleLastNameChange(e)}
                    onChange={handleLastNameChange}
                />
                <TextField 
                    label="Father Name"
                    variant="filled"
                    size="small"
                    helperText={fatherNameErrors}
                    value={fatherName}
                    required={true}
                    inputRef={fatherNameRef}
                    error={fatherNameErrors.length !== 0}
                    margin="dense"
                    onFocus={(e) => handleFatherNameChange(e)}
                    onChange={handleFatherNameChange}
                />
                <TextField
                    label="Age"
                    variant="filled"
                    value={age}
                    required
                    helperText={ageErrors}
                    error={ageErrors.length !== 0}
                    inputRef={ageRef}
                    margin="dense"
                    onFocus={(e) => handleAgeChange(e)}
                    onChange={handleAgeChange}
                />
                <FormControl>
                    <InputLabel id="role-registration-select-label">Role</InputLabel>
                    <Select
                        label="Role"
                        id="role-registration-select"
                        labelId="role-registration-select-label"
                        variant="filled"
                        required
                        value={role}
                        inputRef={roleRef}
                        error={roleErrors.length !== 0}
                        onChange={handleRoleChange}
                    >
                        { getRolesItems() }
                    </Select>
                    <FormHelperText sx={{color: '#d32f2f'}}>{roleErrors}</FormHelperText>
                </FormControl>
                <TextField
                    label="Email"
                    variant="filled"
                    value={email}
                    required={true}
                    helperText={emailErrors}
                    error={emailErrors.length !== 0}
                    inputRef={emailRef}
                    onFocus={(e) => handleEmailChange(e)}
                    onChange={handleEmailChange}
                />
                <TextField
                    label="Password"
                    variant="filled"
                    type="password"
                    value={password}
                    required={true}
                    helperText={passwordErrors}
                    error={passwordErrors.length !== 0}
                    inputRef={passwordRef}
                    onFocus={(e) => handlePasswordChange(e)}
                    onChange={handlePasswordChange}
                />
                <TextField
                    label="Repeat password"
                    variant="filled"
                    type="password"
                    value={repeatPassword}
                    required={true}
                    helperText={repeatPasswordErrors}
                    error={repeatPasswordErrors.length !== 0}
                    inputRef={repeatPasswordRef}
                    onFocus={(e) => handleRepeatPasswordChange(e)}
                    onChange={handleRepeatPasswordChange}
                />
                
                <Button variant="contained" size="large" onClick={submit}>Register</Button>
                <Typography>
                    Or
                </Typography>
                <Button sx={loginButtonStyle} 
                        variant="contained" 
                        size="large" 
                        onClick={redirectToLogin}>
                    Login
                </Button>
            </Box>
        </>
    )
}
