export const createTenantButtonStyle = {
    "&": {
        backgroundColor: "darkcyan"
    },
    "&:hover": {
        backgroundColor: "darkorange"
    }
}

export const formStyle = {
    '&': {
        display: "flex",
        flexDirection: "row",
        alignItems: "center",
        justifyContent: "center",
        height: '70vh'
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

export const rowStyles = {
    '&': {
        display: 'flex', 
        flexDirection: 'row', 
        width: '100vh'
    }
}