import Box from "@mui/material/Box";
import PageBox from "../../../components/wrappers/PageBox";
import Typography from "@mui/material/Typography";
import { useCult } from "../../../hooks/useTranslator";
import { observer } from "mobx-react-lite";
import { ReactNode, useEffect } from "react";
import { useStore } from "../../../api/stores/StoresManager";
import { useLoader } from "../../../hooks/useLoader";
import { Button, TableBody, TableCell, TableRow } from "@mui/material";
import { buttonHoverStyles } from "../../../styles/ButtonStyles";
import EIcon from "../../../components/wrappers/EIcon";
import TableHead from "@material-ui/core/TableHead";
import { buttonsBox } from "../../lessons/LessonsManagerStyles";
import Loader from "../../../components/hoc/loading/Loader";
import { toast } from "react-toastify";
import { ETableCell } from "../../../components/wrappers/ETable/ETableCell";
import { ETableRow } from "../../../components/wrappers/ETable/ETableRow";
import { ETable } from "../../../components/wrappers/ETable/ETable";


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

    const accept = async (userId: string) => 
        await tenantStore.acceptAccessRequest(userId)
            .then(() => toast.success(translator('toasts.request-accepted')));

    const decline = async (userId: string) => 
        await tenantStore.declineAccessRequest(userId)
            .then(() => toast.success(translator('toasts.request-declined')));

    const renderRequests = () => {
        const result: ReactNode[] = [];

        tenantStore.accessRequests?.forEach(x => {
            result.push(
                <ETableRow>
                    <ETableCell>
                        <Box>
                            <b>{translator("labels.full-name")}:</b> {x.name} {x.lastName} {x.fatherName} 
                        </Box>
                        <Box>
                            <b>{translator("labels.email")}:</b> {x.login}
                        </Box>
                    </ETableCell>
                    <ETableCell>
                        <Box sx={buttonsBox}>
                            <Button sx={buttonHoverStyles} variant="contained" onClick={() => accept(x.id)}>
                                <Typography>
                                    {translator('buttons.accept-request')}
                                </Typography>
                                <EIcon type='add'/>
                            </Button>
                            <Button sx={{...buttonHoverStyles, ml: 1}} variant="contained"  onClick={() => decline(x.id)}>
                                <Typography>
                                    {translator('buttons.decline-request')}
                                </Typography>
                                <EIcon type='cancel'/>
                            </Button>
                        </Box>
                    </ETableCell>
                </ETableRow>
            );
        });

        return result;
    };

    return (
        <PageBox>
            <Loader type="spin" replace>                
                <ETable tableName='labels.tenant-requests'>
                    <TableHead>
                        <ETableRow headRow>
                            <ETableCell headCell colSpan={2} columnName='labels.user-info'/>
                        </ETableRow>
                    </TableHead>
                    <TableBody>
                        {renderRequests()}
                    </TableBody>
                </ETable>
            </Loader>
        </PageBox>
    );
});
