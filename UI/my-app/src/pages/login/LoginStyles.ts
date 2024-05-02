export const MainBoxStyle = {
    display: "flex",
    flexDirection: "row",
    alignItems: "center",
    justifyContent: "center",
    "& .MuiTextField-root": { m: 1 },
    "& button": { my: 1 },
    "& .MuiLink-root": { fontSize: "1.2rem" },
}

export const InputsBoxStyle = {
    "&": {
        display: "flex",
        flexDirection: "column",
        alignItems: "center",
        justifyContent: "center",
        width: '50vh'
    },
    "& .MuiTextField-root": { width: '300px' },
}

export const RegisterButtonStyle = {
    "&": {
        backgroundColor: "brown"
    },
    "&:hover": {
        backgroundColor: "red"
    }
}