import TextField, { TextFieldPropsSizeOverrides } from "@mui/material/TextField"
import { OverridableStringUnion } from "@mui/types"
import { HTMLInputTypeAttribute } from "react";


type ETextFieldProps = {
    label: string,
    inputProvider: any,
    required: boolean,
    type?: HTMLInputTypeAttribute | undefined,
    disabled?: boolean | undefined,
    size?: OverridableStringUnion<"small" | "medium", TextFieldPropsSizeOverrides> | undefined
}

export const ETextField = (props: ETextFieldProps) => {
    return (
        <TextField 
            {...props}
            variant="filled"
            value={props.inputProvider.value}
            helperText={props.inputProvider.errors.current}
            error={props.inputProvider.errors.current !== ''}
            inputRef={props.inputProvider.ref}
            onFocus={props.inputProvider.handleChange}
            onChange={props.inputProvider.handleChange}
        />
    );
}