import { Avatar, Button, TextField, Typography } from "@mui/material";
import { Box } from "@mui/system";
import { useEffect, useState, useRef } from "react";
import { useStore } from "../../api/stores/StoresManager";
import EditIcon from '@mui/icons-material/Edit';
import SaveIcon from '@mui/icons-material/Save';
import { mainBoxStyle, 
         profileBoxStyle, 
         avatarStyle, 
         userInfoBlocks,
         userInfoSubSetBlock} from "./UserProfileStyles";
import { buttonImageIconStyles,
         buttonHoverStyles, 
         buttonBoxStyles } from "../../styles/ButtonStyles";
import { toast } from "react-toastify";
import React from "react";
import { Role, UserUpdateModel } from "../../models/Users";
import { getEnumKey } from "../../utils/Utils";

const EMAIL_REGEX = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/
type Focus = React.FocusEvent<HTMLInputElement | HTMLTextAreaElement, Element>; 

export default function UserProfile() {
    const passwordSecret = "**********";
    const { userStore } = useStore();
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
    
    useEffect(() => {
        userStore.getAutenticatedUserInfo();
    }, [userStore]);
    
    const setProfileChanging = () => {
        setChangeMode(!changeMode);
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
            tenantId: null,
            name: name,
            lastName: lastName, 
            fatherName: fatherName,
            password: password === passwordSecret || !password ? null : password
        };

        const result = await userStore.updateUserInfo(user);
        
        if(result.isSuccessful) {
            toast.success('Profile was updated');
        } 
    }

    return(
        <Box sx={mainBoxStyle}>
            <Box sx={profileBoxStyle}>
                <Avatar sx={avatarStyle}>
                    {currentUser?.name[0].toUpperCase()}
                    {currentUser?.lastName[0].toUpperCase()}
                </Avatar>

                <Box sx={buttonBoxStyles}>
                    <Button
                        sx={buttonHoverStyles}   
                        variant="contained"   
                        onClick={setProfileChanging}
                        disabled={!changeMode}             
                    >
                        Change
                        <Avatar sx={buttonImageIconStyles}>
                            <EditIcon />
                        </Avatar>
                    </Button>

                    <Button
                        sx={buttonHoverStyles}   
                        variant="contained"   
                        onClick={submit} 
                        disabled={changeMode}          
                    >
                        Save
                        <Avatar sx={buttonImageIconStyles}>
                            <SaveIcon/>
                        </Avatar>
                    </Button>
                </Box>
            </Box>
            <Box sx={userInfoBlocks}>
                <Box sx={userInfoSubSetBlock}>
                    <Typography variant='h5'> Personal Info: </Typography>
                    <TextField 
                        label="First Name"
                        variant="filled"
                        size="small"
                        helperText={firstNameErrors}
                        value={name}
                        required={true}
                        disabled={changeMode}
                        inputRef={firstNameRef}
                        error={firstNameErrors.length !== 0}
                        margin="dense"
                        onFocus={(e) => handleFirstNameChange(e)}
                        onChange={handleFirstNameChange}
                    />
                    <TextField 
                        label="Last Name"
                        variant="filled"
                        size="small"
                        helperText={lastNameErrors}
                        value={lastName}
                        required={true}
                        disabled={changeMode}
                        inputRef={lastNameRef}
                        error={lastNameErrors.length !== 0}
                        margin="dense"
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
                        disabled={changeMode}
                        inputRef={fatherNameRef}
                        error={fatherNameErrors.length !== 0}
                        margin="dense"
                        onFocus={(e) => handleFatherNameChange(e)}
                        onChange={handleFatherNameChange}
                    />
                    <TextField 
                        label="Age"
                        variant="filled"
                        size="small"
                        helperText={ageErrors}
                        value={age}
                        required={false}
                        disabled={changeMode}
                        inputRef={ageRef}
                        error={ageErrors.length !== 0}
                        margin="dense"
                        onFocus={(e) => handleAgeChange(e)}
                        onChange={handleAgeChange}
                    />
                    <hr/>
                </Box>
                <Box sx={userInfoSubSetBlock}>
                    <Typography variant='h5'> Credentials: </Typography>
                    <TextField 
                        label="Email"
                        variant="filled"
                        size="small"
                        helperText={emailErrors}
                        value={email}
                        required={true}
                        disabled={changeMode}
                        inputRef={loginRef}
                        error={emailErrors.length !== 0}
                        margin="dense"
                        onFocus={(e) => handleEmailChange(e)}
                        onChange={handleEmailChange}
                    />
                    <TextField 
                        label="Password"
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
                        onFocus={(e) => handlePasswordChange(e)}
                        onChange={handlePasswordChange}
                    />
                    <hr/>
                </Box>
                <Box sx={userInfoSubSetBlock}>
                    <Typography variant='h5'> Tenant Info: </Typography>
                    <TextField 
                        label="Role"
                        variant="filled"
                        size="small"
                        value={getEnumKey(Role, userStore.user?.role)}
                        required={false}
                        disabled
                        margin="dense"
                    />
                    <TextField 
                        label="Tenant"
                        variant="filled"
                        size="small"
                        value={userStore.user?.tenant.tenantName}
                        required={false}
                        disabled
                        margin="dense"
                    />
                    <TextField 
                        label="Group"
                        variant="filled"
                        size="small"
                        value={userStore.user?.group.title}
                        required={false}
                        disabled
                        margin="dense"
                    />
                    <hr/>
                </Box>
            </Box>
        </Box>
    );
}