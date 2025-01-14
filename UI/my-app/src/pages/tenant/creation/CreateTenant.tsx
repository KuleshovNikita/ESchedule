import { useNavigate } from "react-router-dom";
import { useStore } from "../../../api/stores/StoresManager";
import { toast } from "react-toastify";
import { useCult } from "../../../hooks/useTranslator";
import { TenantCreateModel, TenantSettingsCreateModel } from "../../../models/Tenants";
import Box from "@mui/material/Box";
import Button from "@mui/material/Button";
import Loader from "../../../components/hoc/loading/Loader";
import { useLoader } from "../../../hooks/useLoader";
import { createTenantButtonStyle, formStyle, rowStyles } from "./TenantStyles";
import PageBox from "../../../components/wrappers/PageBox";
import { useInput } from "../../../hooks/inputHooks/useInput";
import { ETextField } from "../../../components/wrappers/ETextField";
import { useRenderTrigger } from "../../../hooks/useRenderTrigger";
import { useInputValidator } from "../../../hooks/inputHooks/useInputValidator";

export function CreateTenant() {
    const navigate = useNavigate();
    const { translator } = useCult();
    const loader = useLoader();
    const rerender = useRenderTrigger();

    const hasErrors = useInputValidator();

    const tenantNameInput = useInput('text');
    const studyDayStartTimeInput = useInput('text');
    const lessonDurationTimeInput = useInput('text');
    const breaksDurationTimeInput = useInput('text');

    const { tenantStore } = useStore();

    const submit = async () => {
        if (hasErrors(tenantNameInput, studyDayStartTimeInput, lessonDurationTimeInput, breaksDurationTimeInput)) {
            rerender();
            return;
        }

        const settings: TenantSettingsCreateModel = {
            studyDayStartTime: studyDayStartTimeInput.value,
            lessonDurationTime: lessonDurationTimeInput.value,
            breaksDurationTime: breaksDurationTimeInput.value,
        }
        const tenant: TenantCreateModel = { 
            name: tenantNameInput.value,
            settings: settings
        };

        loader.show();

        await tenantStore.createTenant(tenant)
            .then(() => toast.success(translator('toasts.tenant-created')))
            .then(() => loader.hide())
            .then(() => navigate('/logout'))
            .catch(err => loader.hide());
    };

    return (
        <Loader>
            <PageBox>
                <Box
                    component="form"
                    sx={formStyle}
                    noValidate
                    autoComplete="off"
                >    
                    <Box>
                        <Box sx={rowStyles}>   
                            <ETextField 
                                label={translator('labels.tenant-name')} 
                                inputProvider={tenantNameInput} 
                                required={true}                                
                            /> 
                        </Box>
                        <Box sx={rowStyles}>  
                            <ETextField 
                                label={translator('labels.study-day-start-time')} 
                                inputProvider={studyDayStartTimeInput} 
                                required={true}  
                                type="time"                              
                            />
                            <ETextField 
                                label={translator('labels.lesson-duration-time')} 
                                inputProvider={lessonDurationTimeInput} 
                                required={true}
                                type="time"                                
                            />
                            <ETextField 
                                label={translator('labels.breaks-duration-time')} 
                                inputProvider={breaksDurationTimeInput} 
                                required={true}  
                                type="time"                              
                            />
                        </Box>  
                    </Box>
                    <Box>
                        <Button sx={createTenantButtonStyle} 
                            variant="contained" 
                            size="large" 
                            onClick={submit}>
                                {translator('buttons.create-tenant')}
                        </Button>
                    </Box>
                </Box>
            </PageBox>
        </Loader>
    );
}