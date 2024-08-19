import { TableRow } from "@mui/material";
import { headRowStyle } from "../../../styles/TableStyles";

type ETableRowProps = {
    children?: any;
    headRow?: boolean;
}

export const ETableRow = (props: ETableRowProps) => {
    return(
        <TableRow sx={ props.headRow ? headRowStyle : null}>
            {props.children}
        </TableRow>
    );
} 