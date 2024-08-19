import { Box } from "@mui/system";
import { useEffect, useState, useRef } from "react";
import { useStore } from "../../api/stores/StoresManager";
import { mainBoxStyle, 
         profileBoxStyle, 
         avatarStyle, 
         userInfoBlocks,
         userInfoSubSetBlock} from "./UserProfileStyles";
import { buttonHoverStyles, 
         buttonBoxStyles } from "../../styles/ButtonStyles";
import { toast } from "react-toastify";
import React from "react";
import { Role, UserUpdateModel } from "../../models/Users";
import { getEnumKey } from "../../utils/Utils";
import { useCult } from "../../hooks/Translator";
import { useNavigate } from "react-router-dom";
import Avatar from "@mui/material/Avatar";
import Button from "@mui/material/Button";
import Typography from "@mui/material/Typography";
import TextField from "@mui/material/TextField";
import EIcon from "../../components/wrappers/EIcon";
import PageBox from "../../components/wrappers/PageBox";
import PopupForm from "../../components/modalWindow/PopupForm";
import RequestTenantAccess from "../../components/modalWindow/tenant/RequestTenantAccess";

const EMAIL_REGEX = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/
type Focus = React.FocusEvent<HTMLInputElement | HTMLTextAreaElement, Element>; 

export default function UserPage() {
    const passwordSecret = "**********";
    const { userStore, tenantStore } = useStore();
    const { translator } = useCult();
    const navigate = useNavigate();
    const currentUser = userStore.user;

    const [name, setName] = useState(currentUser!.name);
    const [firstNameErrors, setNameErrors] = useState('');

    const [lastName, setLastName] = useState(currentUser!.lastName);
    const [lastNameErrors, setLastNameErrors] = useState('');

    const [fatherName, setFatherName] = useState(currentUser!.fatherName);
    const [fatherNameErrors, setFatherNameErrors] = useState('');

    const [age, setAge] = useState(currentUser!.age);
    const [ageErrors, setAgeErrors] = useState('');

    const [email, setEmail] = useState(currentUser!.login);
    const [emailErrors, setEmailErrors] = useState('');

    const [password, setPassword] = useState(passwordSecret);
    const [passwordErrors, setPasswordErrors] = useState('');

    const [changeMode, setChangeMode] = useState(true);
    const [tenantRequestModal, setTenantRequestModal] = useState(false);
    
    useEffect(() => {
        userStore.getAutenticatedUserInfo();
    }, [userStore]);
    
    const setProfileChanging = () => {
        setChangeMode(!changeMode);
    }

    const resetChanges = () => {
        window.location.reload();
    }

    const firstNameRef = useRef<HTMLInputElement>();
    const lastNameRef = useRef<HTMLInputElement>();
    const fatherNameRef = useRef<HTMLInputElement>();
    const ageRef = useRef<HTMLInputElement>();
    const loginRef = useRef<HTMLInputElement>();
    const passwordRef = useRef<HTMLInputElement>();

    const handleFirstNameChange = (e: Focus) => {
        const firstName = e.target.value;

        if (firstName.length === 0) {
            setNameErrors(translator('input-helpers.first-name-required'));
        } else {
            setNameErrors('');
        }

        setName(firstName);
    }

    const handleLastNameChange = (e: Focus) => {
        const lastName = e.target.value;

        if (lastName.length === 0) {
            setLastNameErrors(translator('input-helpers.last-name-required'));
        } else {
            setLastNameErrors('');
        }

        setLastName(lastName);
    }

    const handleFatherNameChange = (e: Focus) => {
        const fatherName = e.target.value;

        if (fatherName.length === 0) {
            setFatherNameErrors(translator('input-helpers.father-name-required'));
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
            setAgeErrors(translator('input-helpers.minimal-age-5'));
        } else if (age > 99) {
            setAgeErrors(translator('input-helpers.maximal-age-99'));
        } else {
            setAgeErrors('');
        }

        setAge(age);
    }

    const handleEmailChange = (e: Focus) => {
        const email = e.target.value;

        if (email.length === 0) {
            setEmailErrors(translator('input-helpers.email-required'));
        } else if (!email.match(EMAIL_REGEX)) {
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
        const hasAnyErrors = firstNameErrors.length 
                         || lastNameErrors.length 
                         || fatherNameErrors.length 
                         || emailErrors.length
                         || passwordErrors.length;

        return hasAnyErrors;
    }

    const submit = async () => {
        if (hasErrors()) {
            return;
        }

        setProfileChanging();

        const user: UserUpdateModel = {
            id: currentUser?.id!,
            login: email, 
            age: age,
            role: null,
            groupId: null,
            name: name,
            lastName: lastName, 
            fatherName: fatherName,
            password: password === passwordSecret || !password ? null : password
        };

        await userStore.updateUserInfo(user);
        
        toast.success(translator('toasts.profile-updated'));
    }

    const createTenant = async () => {
        navigate('/createTenant');
    }

    function getGroupLabelName(): React.ReactNode {
        return userStore.user?.role === Role.Teacher 
            ? translator('labels.underlying-group') 
            : translator('labels.group'); 
    }

    function getGroupFieldValue(): unknown {
        return userStore.user?.group?.title 
            ?? translator('words.none');
    }

    const canCreateTenant = () => {
        return userStore.user?.role == Role.Dispatcher && !hasTenant()
    } 

    const hasTenant = () => {
        return tenantStore.tenant != null
    }

    return(
        <PageBox>
            <Box sx={mainBoxStyle}>
                <Box sx={profileBoxStyle}>
                    <Avatar sx={avatarStyle}>
                        {currentUser?.name[0].toUpperCase()}
                        {currentUser?.lastName[0].toUpperCase()}
                    </Avatar>

                    <Box sx={buttonBoxStyles}>
                        {
                            !changeMode
                        ?
                            <Button
                                sx={buttonHoverStyles}   
                                variant="contained"   
                                onClick={resetChanges}          
                            >
                                {translator('buttons.cancel')}
                                <EIcon type='cancel'/>
                            </Button>
                        :
                            <Button
                                sx={buttonHoverStyles}   
                                variant="contained"   
                                onClick={setProfileChanging}         
                            >
                                {translator('buttons.change')}
                                <EIcon type='edit'/>
                            </Button>
                        }

                        <Button
                            sx={buttonHoverStyles}   
                            variant="contained"   
                            onClick={createTenant} 
                            disabled={!canCreateTenant()}     
                        >
                            {translator('buttons.create-tenant')}
                            <EIcon type='build'/>
                        </Button>

                        <Button
                            sx={buttonHoverStyles}   
                            variant="contained"   
                            onClick={() => setTenantRequestModal(true)} 
                            disabled={hasTenant()}     
                        >
                            {translator('buttons.send-tenant-request')}
                            <EIcon type='request'/>
                        </Button>
                        
                        <Button
                            sx={buttonHoverStyles}   
                            variant="contained"   
                            onClick={submit} 
                            disabled={changeMode}          
                        >
                            {translator('buttons.save')}
                            <EIcon type='save'/>
                        </Button>
                    </Box>
                </Box>
                <Box sx={userInfoBlocks}>
                    <Box sx={userInfoSubSetBlock}>
                        <Typography variant='h5'> 
                            {translator('headers.personal-info')}
                        </Typography>
                        <TextField 
                            label={translator('labels.first-name')}
                            variant="filled"
                            size="small"
                            helperText={firstNameErrors}
                            value={name}
                            required={true}
                            disabled={changeMode}
                            inputRef={firstNameRef}
                            error={firstNameErrors.length !== 0}
                            margin="dense"
                            onFocus={(e: Focus) => handleFirstNameChange(e)}
                            onChange={handleFirstNameChange}
                        />
                        <TextField 
                            label={translator('labels.last-name')}
                            variant="filled"
                            size="small"
                            helperText={lastNameErrors}
                            value={lastName}
                            required={true}
                            disabled={changeMode}
                            inputRef={lastNameRef}
                            error={lastNameErrors.length !== 0}
                            margin="dense"
                            onFocus={(e: Focus) => handleLastNameChange(e)}
                            onChange={handleLastNameChange}
                        />
                        <TextField 
                            label={translator('labels.father-name')}
                            variant="filled"
                            size="small"
                            helperText={fatherNameErrors}
                            value={fatherName}
                            required={true}
                            disabled={changeMode}
                            inputRef={fatherNameRef}
                            error={fatherNameErrors.length !== 0}
                            margin="dense"
                            onFocus={(e: Focus) => handleFatherNameChange(e)}
                            onChange={handleFatherNameChange}
                        />
                        <TextField 
                            label={translator('labels.age')}
                            variant="filled"
                            size="small"
                            helperText={ageErrors}
                            value={age}
                            required={true}
                            disabled={changeMode}
                            inputRef={ageRef}
                            error={ageErrors.length !== 0}
                            margin="dense"
                            onFocus={(e: Focus) => handleAgeChange(e)}
                            onChange={handleAgeChange}
                        />
                        <hr/>
                    </Box>
                    <Box sx={userInfoSubSetBlock}>
                        <Typography variant='h5'> 
                            {translator('headers.credentials')}
                        </Typography>
                        <TextField 
                            label={translator('labels.email')}
                            variant="filled"
                            size="small"
                            helperText={emailErrors}
                            value={email}
                            required={true}
                            disabled={changeMode}
                            inputRef={loginRef}
                            error={emailErrors.length !== 0}
                            margin="dense"
                            onFocus={(e: Focus) => handleEmailChange(e)}
                            onChange={handleEmailChange}
                        />
                        <TextField 
                            label={translator('labels.password')}
                            variant="filled"
                            size="small"
                            helperText={passwordErrors}
                            type="password"
                            value={password}
                            required={true}
                            disabled={changeMode}
                            inputRef={passwordRef}
                            error={passwordErrors.length !== 0}
                            margin="dense"
                            onFocus={(e: Focus) => handlePasswordChange(e)}
                            onChange={handlePasswordChange}
                        />
                        <hr/>
                    </Box>
                    <Box sx={userInfoSubSetBlock}>
                        <Typography variant='h5'> 
                            {translator('headers.tenant-info')}
                        </Typography>
                        <TextField 
                            label={translator('labels.role')}
                            variant="filled"
                            size="small"
                            value={translator(getEnumKey(Role, userStore.user?.role))}
                            required={false}
                            disabled
                            margin="dense"
                        />
                        <TextField 
                            label={translator('labels.tenant')}
                            variant="filled"
                            size="small"
                            value={userStore.user?.tenant?.name ?? "None"}
                            required={false}
                            disabled
                            margin="dense"
                        />
                        <TextField 
                            label={getGroupLabelName()}
                            variant="filled"
                            size="small"
                            value={getGroupFieldValue()}
                            required={false}
                            disabled
                            margin="dense"
                        />
                        <hr/>
                    </Box>
                </Box>
            </Box>

            {
                tenantRequestModal
                &&
                <PopupForm closeButtonHandler={() => setTenantRequestModal(false)}>
                    <RequestTenantAccess closeModal={() => setTenantRequestModal(false)}/>
                </PopupForm>
            }
        </PageBox>
    );
}