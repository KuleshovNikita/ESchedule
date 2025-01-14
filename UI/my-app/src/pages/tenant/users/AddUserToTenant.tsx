import { useCult } from "../../../hooks/useTranslator";
import { useEffect, useState } from "react";
import { buttonHoverStyles } from "../../../styles/ButtonStyles";
import { useStore } from "../../../api/stores/StoresManager";
import { toast } from "react-toastify";
import Box from "@mui/material/Box";
import TextField from "@mui/material/TextField";
import Button from "@mui/material/Button";
import EIcon from "../../../components/wrappers/EIcon";
import PageBox from "../../../components/wrappers/PageBox";
import { GUID_REGEX } from "../../../utils/RegexConstants";
import { isGuid } from "../../../utils/Utils";

export default function AddUserToTenant() {
    const { translator } = useCult();
    const { userStore } = useStore();
    const [userCode, setUserCode] = useState('');
    const [error, setError] = useState('');

    useEffect(() => {
        if(isGuid(userCode) && error !== '') {
            setError('');
        } else {
            setError(translator('input-helpers.invalid-user-code-inserted'));
        }
    }, [userCode])

    const addUserToTenant = async () => {
        await userStore.updateUserTenant(userCode)
            .then(() => toast.success(translator('toasts.user-added-to-tenant')));
    }

    return(
        <PageBox>
            <Box>
                <TextField
                    label={translator('labels.user-code')}
                    onChange={(e: any) => setUserCode(e.target.value)}
                    helperText={error}
                    sx={{width: '25%'}}
                />
            </Box>
            <Box>
                <Button 
                    variant='contained' 
                    sx={buttonHoverStyles}
                    disabled={!isGuid(userCode)}
                    onClick={addUserToTenant}
                >
                    {translator('buttons.add')}
                    <EIcon type='add'/>
                </Button>
            </Box>            
        </PageBox>
    )
}