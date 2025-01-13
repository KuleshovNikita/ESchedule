import { useCult } from "../../../hooks/useTranslator";
import Button from "@material-ui/core/Button";
import { buttonHoverStyles } from "../../../styles/ButtonStyles";
import EIcon from "../../wrappers/EIcon";
import { useStore } from "../../../api/stores/StoresManager";
import { RequestTenantAccessModel } from "../../../models/Tenants";
import { toast } from "react-toastify";
import { useInput } from "../../../hooks/useInput";
import { ETextField } from "../../wrappers/ETextField";

const RequestTenantAccess = ({closeModal}: any) => {
    const { translator } = useCult();
    const { userStore, tenantStore } = useStore();
    
    const tenantCodeInput = useInput('guid');

    const sendTenantRequest = async () => {
        if(tenantCodeInput.errors) {
            return;
        }

        const request: RequestTenantAccessModel = {
            userId: userStore.user!.id,
            tenantId: tenantCodeInput.value
        }

        await tenantStore.sendTenantAccessRequest(request)
            .then(() => toast.success(translator('toasts.request-sent')));

        closeModal();
    }
    
    return (<>
        <ETextField
            label={translator('labels.tenant-code')}
            inputProvider={tenantCodeInput}
            required={true}
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