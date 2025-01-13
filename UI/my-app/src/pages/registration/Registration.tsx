import { useEffect, useRef, useState } from "react";
import { useNavigate } from "react-router-dom";
import { useStore } from "../../api/stores/StoresManager";
import { toast } from 'react-toastify';
import { createTenantButtonStyle, loginButtonStyle, formStyle, personalDataRowStyles, buttonsBlockStyles } from "./RegistrationStyles";
import { Role, UserCreateModel } from "../../models/Users";
import { useCult } from "../../hooks/useTranslator";
import MenuItem from "@mui/material/MenuItem";
import TextField from "@mui/material/TextField";
import Box from "@mui/material/Box";
import FormControl from "@mui/material/FormControl";
import Select from "@mui/material/Select";
import InputLabel from "@mui/material/InputLabel";
import Button from "@mui/material/Button";
import Typography from "@mui/material/Typography";
import PageBox from "../../components/wrappers/PageBox";
import { useInput } from "../../hooks/useInput";

export default function RegistrationPage() {
    const { translator } = useCult();

    const [role, setRole] = useState(Role.Pupil);

    const nameInput = useInput('text');
    const lastNameInput = useInput('text');
    const fatherNameInput = useInput('text');
    const ageInput = useInput('number', '5', [5, 99]);
    const emailInput = useInput('email');
    const passwordInput = useInput('text');

    const [repeatPassword, setRepeatPassword] = useState('');
    const [repeatPasswordErrors, setRepeatPasswordErrors] = useState('');

    const { userStore } = useStore();
    const navigate = useNavigate();

    useEffect(() => {
        validateForm();
    }, [repeatPassword]);

    const validateForm = () => {
        validateRepeatPassword();
    }

    const validateRepeatPassword = () => {
        if (repeatPassword.length === 0) {
            setRepeatPasswordErrors(translator('input-helpers.please-repeat-password'));
        } else if (repeatPassword !== passwordInput.value) {
            setRepeatPasswordErrors(translator('input-helpers.passwords-do-not-match'));
        } else {
            setRepeatPasswordErrors('');
        }
    }

    const hasErrors = () => 
        nameInput.errors.length 
        || lastNameInput.errors.length 
        || fatherNameInput.errors.length 
        || ageInput.errors.length 
        || emailInput.errors.length 
        || passwordInput.errors.length 
        || repeatPasswordErrors.length;

    const submit = async () => {
        if (hasErrors()) {
            return;
        }

        const user: UserCreateModel = {
            login: emailInput.value, 
            name: nameInput.value,
            lastName: lastNameInput.value, 
            fatherName: fatherNameInput.value, 
            age: Number(ageInput.value),
            password: passwordInput.value,
            role: role as Role
        };

        await userStore.register(user);
        
        navigate("/login");
        toast.success(translator('toasts.verification-email-sent'));
    }

    const redirectToLogin = () => 
        navigate("/login");

    const redirectToTenantCreator = () => 
        navigate("/createTenant");

    const getRolesItems = () => {
        const values = Object.values(Role).filter(x => typeof x !== 'string') as Role[];
        const result = values.map((vk, key) => <MenuItem key={key} value={vk}>
                                                    {translator('roles.' + Role[vk])}
                                                </MenuItem>
                                );

        return result;
    }

    return (
        <PageBox>
            <Box
                component="form"
                sx={formStyle}
                autoComplete="off"
            >
                <Box>
                    <Box sx={personalDataRowStyles}>
                        <TextField
                            label={translator('labels.first-name')}
                            variant="filled"
                            value={nameInput.value}
                            required={true}
                            helperText={nameInput.errors}
                            error={nameInput.errors.length !== 0}
                            inputRef={nameInput.ref}
                            onFocus={nameInput.handleChange}
                            onChange={nameInput.handleChange}
                        />
                        <TextField
                            label={translator('labels.last-name')}
                            variant="filled"
                            value={lastNameInput.value}
                            required={true}
                            helperText={lastNameInput.errors}
                            error={lastNameInput.errors.length !== 0}
                            onChange={lastNameInput.handleChange}
                            inputRef={lastNameInput.ref}
                            onFocus={lastNameInput.handleChange}
                        />
                        <TextField 
                            label={translator('labels.father-name')}
                            variant="filled"
                            helperText={fatherNameInput.errors}
                            value={fatherNameInput.value}
                            required={true}
                            error={fatherNameInput.errors.length !== 0}
                            margin="dense"
                            inputRef={fatherNameInput.ref}
                            onChange={fatherNameInput.handleChange}
                            onFocus={fatherNameInput.handleChange}
                        />
                    </Box>
                    <Box sx={personalDataRowStyles}>
                        <TextField
                            label={translator('labels.age')}
                            variant="filled"
                            value={ageInput.value}
                            type="number"
                            required
                            helperText={ageInput.errors}
                            error={ageInput.errors.length !== 0}
                            margin="dense"
                            inputRef={ageInput.ref}
                            onChange={ageInput.handleChange}
                            onFocus={ageInput.handleChange}
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
                                onChange={(e: any) => setRole(e.target.value as Role)}
                            >
                                { getRolesItems() }
                            </Select>
                        </FormControl>
                    </Box>
                    <Box sx={personalDataRowStyles}>
                        <TextField
                            label={translator('labels.email')}
                            variant="filled"
                            value={emailInput.value}
                            required={true}
                            helperText={emailInput.errors}
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
                            helperText={passwordInput.errors}
                            error={passwordInput.errors.length !== 0}
                            inputRef={passwordInput.ref}
                            onFocus={passwordInput.handleChange}
                            onChange={passwordInput.handleChange}
                        />
                        <TextField
                            label={translator('labels.repeat-password')}
                            variant="filled"
                            type="password"
                            value={repeatPassword}
                            required={true}
                            helperText={repeatPasswordErrors}
                            error={repeatPasswordErrors.length !== 0}
                            onChange={(e: any) => setRepeatPassword(e.target.value)}
                        />
                    </Box>
                </Box>
                <Box sx={buttonsBlockStyles}>
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
    )
}

