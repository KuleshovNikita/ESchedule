import Table from "@material-ui/core/Table";
import Box from "@mui/material/Box";
import Typography from "@mui/material/Typography";
import { useCult } from "../../../hooks/useTranslator";

type ETableProps = {
    children?: any;
    tableName: string;
}

export const ETable = (props: ETableProps) => {
    const { translator } = useCult();

    return(<>
        <Box>
            <Typography variant="h4" component="h4">
                {translator(props.tableName)}
            </Typography>
        </Box>
        <Table>
            {props.children}
        </Table>
    </>);
} 