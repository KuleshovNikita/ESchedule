import { TableCell } from "@mui/material";
import { cellStyle, checkboxCellStyle, headCellStyle } from "../../../styles/TableStyles";
import Checkbox from "@material-ui/core/Checkbox";
import { checkboxStyle } from "../../../pages/lessons/LessonsManagerStyles";
import { ChangeEvent } from "react";

type CellType = 'checkbox' | undefined;

type ETableCellProps = {
    children?: any;
    headCell?: boolean;
    kind?: CellType;
    onClick?: ((event: ChangeEvent<HTMLInputElement>, checked: boolean) => void) | undefined
}

export const ETableCell = (props: ETableCellProps) => {
    const decideType = (props: ETableCellProps) => {
        console.log(props)
        switch(props.kind) {
            case 'checkbox':
                return (
                    <TableCell sx={checkboxCellStyle}>
                        <Checkbox
                            sx={checkboxStyle}
                            onChange={props.onClick}
                        />    
                    </TableCell>  
                )
            default:
                return(
                    <TableCell sx={props.headCell ? headCellStyle : cellStyle}>
                        {props.children}
                    </TableCell>
                );
        }
    }

    return(decideType(props));
} 