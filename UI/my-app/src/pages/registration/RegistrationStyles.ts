export const loginButtonStyle = {
    "&": {
        backgroundColor: "brown"
    },
    "&:hover": {
        backgroundColor: "red"
    }
}

export const createTenantButtonStyle = {
    "&": {
        backgroundColor: "darkcyan"
    },
    "&:hover": {
        backgroundColor: "darkorange"
    }
}

export const registrationFormStyle = {
    '&': {
        display: 'flex', 
        flexDirection: 'column', 
        alignItems: 'center'
    },
    '& .MuiTextField-root, .MuiFormControl-root': {
        m: 1, 
        width: '25ch'
    },
    '& button': {
        my: 1
    },
    '& .MuiLink-root': {
        fontSize: '1.2rem'
    },
}