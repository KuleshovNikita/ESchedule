import Box from "@mui/material/Box";
import PageBox from "../../../components/wrappers/PageBox";
import Typography from "@mui/material/Typography";
import { useCult } from "../../../hooks/Translator";
import { observer } from "mobx-react-lite";
import { ReactNode, useEffect } from "react";
import { useStore } from "../../../api/stores/StoresManager";
import { useLoader } from "../../../hooks/Loader";
import { Button, TableCell, TableRow } from "@mui/material";
import { buttonHoverStyles } from "../../../styles/ButtonStyles";
import Icon from "../../../components/wrappers/Icon";
import Table from "@material-ui/core/Table";
import TableHead from "@material-ui/core/TableHead";
import { headCellStyle, headRowStyle } from "../../../styles/TableStyles";
import { buttonsBox } from "../../lessons/LessonsManagerStyles";
import Loader from "../../../components/hoc/loading/Loader";


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

    const accept = async (userId: string) => await tenantStore.acceptAccessRequest(userId);

    const decline = async (userId: string) => await tenantStore.declineAccessRequest(userId);

    const renderRequests = () => {
        const result: ReactNode[] = [];

        tenantStore.accessRequests?.forEach(x => {
            result.push(
                <TableRow>
                    <TableCell>
                        <Box>
                            <b>{translator("lables.full-name")}:</b> {x.name} {x.lastName} {x.fatherName} 
                        </Box>
                        <Box>
                            <b>{translator("lables.email")}:</b> {x.login}
                        </Box>
                    </TableCell>
                    <TableCell>
                        <Box sx={buttonsBox}>
                            <Button sx={buttonHoverStyles} variant="contained" onClick={() => accept(x.id)}>
                                <Icon type='add'/>
                                <Typography>
                                    {translator('labels.accept-request')}
                                </Typography>
                            </Button>
                            <Button sx={{...buttonHoverStyles, ml: 1}} variant="contained"  onClick={() => decline(x.id)}>
                                <Icon type='cancel'/>
                                <Typography>
                                    {translator('labels.decline-request')}
                                </Typography>
                            </Button>
                        </Box>
                    </TableCell>
                </TableRow>
            );
        });

        return result;
    };

    return (
        <PageBox>
            <Loader type="spin" replace>                
                <Box>
                    <Typography variant="h3" component="h3">
                        {translator('labels.tenantRequests')}
                    </Typography>
                </Box>
                <Table>
                    <TableHead>
                        <TableRow sx={headRowStyle}>
                            <TableCell sx={{...headCellStyle}} colSpan={2}>
                                <Typography variant="h6">
                                    <b>{translator('labels.user-info')}</b>
                                </Typography>
                            </TableCell>
                        </TableRow>
                    </TableHead>
                    {renderRequests()}
                </Table>
            </Loader>
        </PageBox>
    );
});
