import { TextField } from "@mui/material";
import { useRef, useState } from "react";
import { useCult } from "../../../hooks/useTranslator";
import Button from "@material-ui/core/Button";
import { buttonHoverStyles } from "../../../styles/ButtonStyles";
import EIcon from "../../wrappers/EIcon";
import { useStore } from "../../../api/stores/StoresManager";
import { RequestTenantAccessModel } from "../../../models/Tenants";
import { toast } from "react-toastify";
import { useInput } from "../../../hooks/useInput";

const RequestTenantAccess = ({closeModal}: any) => {
    const { translator } = useCult();
    const { userStore, tenantStore } = useStore();
    
    const { ref, value, errors, handleChange } = useInput('guid');

    const sendTenantRequest = async () => {
        if(errors) {
            return;
        }

        const request: RequestTenantAccessModel = {
            userId: userStore.user!.id,
            tenantId: value
        }

        await tenantStore.sendTenantAccessRequest(request)
            .then(() => toast.success(translator('toasts.request-sent')));

        closeModal();
    }
    
    return (<>
        <TextField 
            label={translator('labels.tenant-code')}
            variant="filled"
            helperText={errors.current}
            value={value}
            required={true}
            inputRef={ref}
            error={errors.current !== ''}
            margin="dense"
            onFocus={handleChange}
            onChange={handleChange}
        />
        <Button
            sx={buttonHoverStyles}   
            variant="contained"   
            onClick={sendTenantRequest}     
        >
            {translator('buttons.send')}
            <EIcon type='request'/>
        </Button>
    </>);
}

export default RequestTenantAccess;