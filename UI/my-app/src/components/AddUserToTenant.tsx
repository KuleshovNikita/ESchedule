import { Box, Button, TextField } from "@mui/material";
import { useCult } from "../hooks/Translator";
import { useEffect, useState } from "react";
import { buttonHoverStyles, buttonImageIconStyle } from "../styles/ButtonStyles";
import AddCircleIcon from '@mui/icons-material/AddCircle';
import { useStore } from "../api/stores/StoresManager";
import { UserUpdateModel } from "../models/Users";
import { toast } from "react-toastify";

export default function AddUserToTenant() {
    const { translator } = useCult();
    const { userStore, tenantStore } = useStore();
    const [userCode, setUserCode] = useState('');
    const [error, setError] = useState('');

    useEffect(() => {
        if(isGuid() && error !== '') {
            setError('');
        } else {
            setError(translator('input-helpers.invalid-user-code-inserted'));
        }
    }, [userCode])

    const isGuid = () => {
        return /^[{]?[0-9a-fA-F]{8}-([0-9a-fA-F]{4}-){3}[0-9a-fA-F]{12}[}]?$/.test(userCode);
    }

    const addUserToTenant = async () => {
        const userUpdate: UserUpdateModel = {
            name: null,
            lastName: null,
            fatherName: null,
            login: null,
            password: null,
            age: null,
            role: null,
            groupId: null,
            tenantId: tenantStore.tenant?.id as string,
            id: userCode
        }

        const response = await userStore.updateUserInfo(userUpdate);

        if(response.isSuccessful) {
            toast.success(translator('toasts.user-added-to-tenant'));
        }
    }

    return(
        <Box>
            <Box>
                <TextField
                    label={translator('labels.user-code')}
                    onChange={(e) => setUserCode(e.target.value)}
                    helperText={error}
                    sx={{width: '25%'}}
                />
            </Box>
            <Box>
                <Button 
                    variant='contained' 
                    sx={buttonHoverStyles}
                    disabled={!isGuid()}
                    onClick={addUserToTenant}
                >
                    {translator('buttons.add')}
                    <AddCircleIcon sx={buttonImageIconStyle} />
                </Button>
            </Box>
        </Box>
    )
}