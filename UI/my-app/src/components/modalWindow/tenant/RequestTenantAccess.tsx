import { TextField } from "@mui/material";
import { useRef, useState } from "react";
import { useCult } from "../../../hooks/useTranslator";
import Button from "@material-ui/core/Button";
import { buttonHoverStyles } from "../../../styles/ButtonStyles";
import EIcon from "../../wrappers/EIcon";
import { useStore } from "../../../api/stores/StoresManager";
import { RequestTenantAccessModel } from "../../../models/Tenants";
import { toast } from "react-toastify";

var GUID_REGEX = /^[0-9a-f]{8}-[0-9a-f]{4}-[1-5][0-9a-f]{3}-[89ab][0-9a-f]{3}-[0-9a-f]{12}$/i;
type Focus = React.FocusEvent<HTMLInputElement | HTMLTextAreaElement, Element>; 

const RequestTenantAccess = ({closeModal}: any) => {
    const { translator } = useCult();

    const { userStore, tenantStore } = useStore();
    
    const [value, setValue] = useState('');
    const [errors, setErrors] = useState('');

    const tenantRef = useRef<HTMLInputElement>();

    const handleChange = (e: Focus) => {
        const value = e.target.value;

        if (value.length === 0) {
            setErrors(translator('input-helpers.field-required'));
        } else if (!value.match(GUID_REGEX)) {
            setErrors(translator('input-helpers.field-wrong-format'));
        } else {
            setErrors('');
        }

        setValue(value);
    }

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
            helperText={errors}
            value={value}
            required={true}
            inputRef={tenantRef}
            error={errors.length !== 0}
            margin="dense"
            onFocus={(e: Focus) => handleChange(e)}
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