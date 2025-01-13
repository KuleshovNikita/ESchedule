import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { useStore } from "../../api/stores/StoresManager";
import { toast } from 'react-toastify';
import { createTenantButtonStyle, loginButtonStyle, formStyle, personalDataRowStyles, buttonsBlockStyles } from "./RegistrationStyles";
import { Role, UserCreateModel } from "../../models/Users";
import { useCult } from "../../hooks/useTranslator";
import MenuItem from "@mui/material/MenuItem";
import Box from "@mui/material/Box";
import FormControl from "@mui/material/FormControl";
import Select from "@mui/material/Select";
import InputLabel from "@mui/material/InputLabel";
import Button from "@mui/material/Button";
import Typography from "@mui/material/Typography";
import PageBox from "../../components/wrappers/PageBox";
import { useInput } from "../../hooks/useInput";
import { ETextField } from "../../components/wrappers/ETextField";
import { useRenderTrigger } from "../../hooks/useRenderTrigger";

export default function RegistrationPage() {
    const { translator } = useCult();

    const [role, setRole] = useState(Role.Pupil);
    const rerender = useRenderTrigger();

    const nameInput = useInput('text');
    const lastNameInput = useInput('text');
    const fatherNameInput = useInput('text');
    const ageInput = useInput('number', '5', [5, 99]);
    const emailInput = useInput('email');
    const passwordInput = useInput('text');
    const passwordRepeatInput = useInput('repeatPassword', '', [passwordInput.value]);

    const { userStore } = useStore();
    const navigate = useNavigate();

    const hasErrors = () => 
        nameInput.errors.current !== ''  
        || lastNameInput.errors.current !== '' 
        || fatherNameInput.errors.current !== '' 
        || ageInput.errors.current !== '' 
        || emailInput.errors.current !== ''  
        || passwordInput.errors.current !== ''  
        || passwordRepeatInput.errors.current !== '';

    const validateInputs = () => {
        nameInput.ref.current?.focus();
        lastNameInput.ref.current?.focus();
        fatherNameInput.ref.current?.focus();
        ageInput.ref.current?.focus();
        emailInput.ref.current?.focus();
        passwordInput.ref.current?.focus();
        passwordRepeatInput.ref.current?.focus();

        passwordRepeatInput.validate(passwordRepeatInput.value, passwordInput.value);
    }

    const submit = async () => {
        validateInputs();

        if (hasErrors()) {
            rerender();
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
                        <ETextField
                            label={translator('labels.first-name')}
                            inputProvider={nameInput}
                            required={true}
                        />
                        <ETextField
                            label={translator('labels.last-name')}
                            inputProvider={lastNameInput}
                            required={true}
                        />
                        <ETextField 
                            label={translator('labels.father-name')}
                            inputProvider={fatherNameInput}
                            required={true}
                        />
                    </Box>
                    <Box sx={personalDataRowStyles}>
                        <ETextField
                            label={translator('labels.age')}
                            inputProvider={ageInput}
                            required={true}
                            type="number"
                        />
                        <FormControl>
                            <InputLabel id="role-registration-select-label">
                                {translator('labels.role')}
                            </InputLabel>
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
                        <ETextField
                            label={translator('labels.repeat-password')}
                            inputProvider={passwordRepeatInput}
                            required={true}
                            type="password"
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

