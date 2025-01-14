import { Box } from "@mui/system";
import { useEffect, useState } from "react";
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
import { useCult } from "../../hooks/useTranslator";
import { useNavigate } from "react-router-dom";
import Avatar from "@mui/material/Avatar";
import Button from "@mui/material/Button";
import Typography from "@mui/material/Typography";
import TextField from "@mui/material/TextField";
import EIcon from "../../components/wrappers/EIcon";
import PageBox from "../../components/wrappers/PageBox";
import PopupForm from "../../components/modalWindow/PopupForm";
import RequestTenantAccess from "../../components/modalWindow/tenant/RequestTenantAccess";
import { useInput } from "../../hooks/inputHooks/useInput";
import { ETextField } from "../../components/wrappers/ETextField";
import { useRenderTrigger } from "../../hooks/useRenderTrigger";
import { useInputValidator } from "../../hooks/inputHooks/useInputValidator";
import pageRoutes from "../../utils/RoutesProvider";

export default function UserPage() {
    const passwordSecret = "**********";
    const { userStore, tenantStore } = useStore();
    const { translator } = useCult();
    const navigate = useNavigate();
    const currentUser = userStore.user;

    const rerender = useRenderTrigger();
    const hasErrors = useInputValidator();

    const firstNameInput = useInput('text', currentUser!.name);
    const lastNameInput = useInput('text', currentUser!.lastName);
    const fatherNameInput = useInput('text', currentUser!.fatherName);
    const ageInput = useInput('number', currentUser!.age.toString());
    const emailInput = useInput('email', currentUser!.login);
    const passwordInput = useInput('text', passwordSecret);

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

    const submit = async () => {
        if (hasErrors(
            firstNameInput,
            lastNameInput,
            fatherNameInput,
            ageInput,
            emailInput,
            passwordInput
        )) {
            rerender();
            return;
        }

        setProfileChanging();

        const user: UserUpdateModel = {
            id: currentUser?.id!,
            login: emailInput.value, 
            age: ageInput.value as unknown as number,
            role: null,
            groupId: null,
            name: firstNameInput.value,
            lastName: lastNameInput.value, 
            fatherName: fatherNameInput.value,
            password: passwordInput.value === passwordSecret || !passwordInput.value ? null : passwordInput.value
        };

        await userStore.updateUserInfo(user);
        
        toast.success(translator('toasts.profile-updated'));
    }

    const createTenant = async () => {
        navigate(pageRoutes.createTenant);
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
                        <ETextField 
                            label={'labels.first-name'} 
                            inputProvider={firstNameInput} 
                            required={true}
                            disabled={changeMode}
                            size="small"                        
                        />
                        <ETextField 
                            label={'labels.last-name'} 
                            inputProvider={lastNameInput} 
                            required={true}
                            disabled={changeMode}
                            size="small"                        
                        />
                        <ETextField 
                            label={'labels.father-name'} 
                            inputProvider={fatherNameInput} 
                            required={true}
                            disabled={changeMode}
                            size="small"                        
                        />
                        <ETextField 
                            label={'labels.age-name'} 
                            inputProvider={ageInput} 
                            required={true}
                            disabled={changeMode}
                            size="small"                        
                        />
                        <hr/>
                    </Box>
                    <Box sx={userInfoSubSetBlock}>
                        <Typography variant='h5'> 
                            {translator('headers.credentials')}
                        </Typography>
                        <ETextField 
                            label={'labels.email'} 
                            inputProvider={emailInput} 
                            required={true}
                            disabled={changeMode}
                            size="small"                        
                        />
                        <ETextField 
                            label={'labels.password'} 
                            inputProvider={passwordInput} 
                            required={true}
                            disabled={changeMode}
                            size="small"                        
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