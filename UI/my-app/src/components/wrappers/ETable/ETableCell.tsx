import { TableCell } from "@mui/material";
import { cellStyle, checkboxCellStyle, headCellStyle } from "./TableStyles";
import Checkbox from "@material-ui/core/Checkbox";
import { checkboxStyle } from "../../../pages/lessons/LessonsManagerStyles";
import { ChangeEvent } from "react";
import Typography from "@material-ui/core/Typography";
import { useCult } from "../../../hooks/useTranslator";

type CellType = 'checkbox' | undefined;

type ETableCellProps = {
    children?: any;
    headCell?: boolean;
    kind?: CellType;
    onClick?: ((event: ChangeEvent<HTMLInputElement>, checked: boolean) => void) | undefined;
    colSpan?: number;
    columnName?: string;
}

export const ETableCell = (props: ETableCellProps) => {
    const { translator } = useCult();

    const decideType = (props: ETableCellProps) => {
        switch(props.kind) {
            case 'checkbox':
                return (
                    <TableCell sx={checkboxCellStyle} colSpan={props.colSpan ?? 1}>
                        <Checkbox
                            sx={checkboxStyle}
                            onChange={props.onClick}
                        />    
                    </TableCell>  
                )
            default:
                return(
                    <TableCell 
                        sx={props.headCell ? headCellStyle : cellStyle} 
                        colSpan={props.colSpan ?? 1}
                    >
                        {
                            props.headCell 
                        && 
                            <Typography variant="h6">
                                <b>{translator(props.columnName!)}</b>
                            </Typography>
                        }
                        {props.children}
                    </TableCell>
                );
        }
    }

    return(decideType(props));
} 