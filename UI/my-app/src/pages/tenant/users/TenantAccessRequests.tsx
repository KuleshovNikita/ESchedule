import Box from "@mui/material/Box";
import PageBox from "../../../components/wrappers/PageBox";
import Typography from "@mui/material/Typography";
import { useCult } from "../../../hooks/Translator";
import { observer } from "mobx-react-lite";
import { ReactNode, useEffect } from "react";
import { useStore } from "../../../api/stores/StoresManager";
import { useLoader } from "../../../hooks/Loader";
import { Button } from "@mui/material";
import { buttonHoverStyles } from "../../../styles/ButtonStyles";
import Icon from "../../../components/wrappers/Icon";


export const TenantAccessRequests = observer(() => {
    const { translator } = useCult();
    const { tenantStore } = useStore();
    const loader = useLoader();

    useEffect(() => {
        const fetchAccessRequests = async () => await tenantStore.getTenantAccessRequests()
            .then(res => tenantStore.accessRequests = res)
            .then(() => loader.hide());

        loader.show();
        fetchAccessRequests()
            .then(() => loader.hide());
    });

    const renderRequests = () => {
        const result: ReactNode[] = [];

        tenantStore.accessRequests?.forEach(x => {
            result.push(
                <Box>
                    <Box>
                        {x.name} {x.lastName} {x.fatherName} {x.login}
                    </Box>
                    <Box>
                        <Button sx={buttonHoverStyles} variant="contained">
                            <Icon type='add'/>
                            <Typography>
                                {translator('labels.accept-request')}
                            </Typography>
                        </Button>
                        <Button sx={buttonHoverStyles} variant="contained">
                            <Icon type='cancel'/>
                            <Typography>
                                {translator('labels.decline-request')}
                            </Typography>
                        </Button>
                    </Box>
                </Box>
            );
        });

        return result;
    };

    return (
        <PageBox>
            <Box>
                <Box>
                    <Typography variant="h3" component="h3">
                        {translator('labels.tenantRequests')}
                    </Typography>
                </Box>
                {renderRequests()}
            </Box>
        </PageBox>
    );
});
