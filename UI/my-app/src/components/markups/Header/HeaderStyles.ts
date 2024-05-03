export const headerLeftSideBoxStyle = {
    display: 'flex',
    flexDirection: 'row',
    ml: 2,
    mr: 2,
}

export const labelStyles = {
    display: 'flex',
    flexDirection: 'column'
}

export const cultureSelectStyle = {
    mt: 2,
    mb: 3,
    mr: 1.8,
    width: "150px",
    '& .MuiInputBase-input': {
        backgroundColor: 'white',
    },
}

export const titleTextStyle = {
    display: "inline-block",
    color: 'white'
}

export const tenantTitleStyle = {
    ...titleTextStyle,
    textAlign: 'center'
}

export const navigationButtonsStyle = { 
    display: "flex", 
    justifyContent: "end",
    mt: 2,
    mb: 2,
}

export const headerNavButtonStyle = {
    bgcolor: "white",
    color: "orange",
    height: '60px',
    mr: 1,
    "&:hover": {
        bgcolor: "brown"
    },
}

export const headerIconStyle = {
    color: 'orange'
}

export const profileNavButtonStyle = {
    width: '60px'
}

export const headerBox = {
    bgcolor: "orange",
    display: "grid",
    gridTemplateColumns: "6fr 1fr",
    height: '90px'
}