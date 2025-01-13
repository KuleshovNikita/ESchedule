import TextField from "@mui/material/TextField"
import { HTMLInputTypeAttribute } from "react";


type ETextFieldProps = {
    label: string,
    inputProvider: any,
    required: boolean,
    type?: HTMLInputTypeAttribute | undefined
}

export const ETextField = (props: ETextFieldProps) => {
    return (
        <TextField 
            label={props.label}
            variant="filled"
            value={props.inputProvider.value}
            required={props.required}
            type={props.type ?? 'text'}
            helperText={props.inputProvider.errors.current}
            error={props.inputProvider.errors.current !== ''}
            inputRef={props.inputProvider.ref}
            onFocus={props.inputProvider.handleChange}
            onChange={props.inputProvider.handleChange}
        />
    );
}