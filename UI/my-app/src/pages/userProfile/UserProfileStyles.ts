import { deepOrange } from "@mui/material/colors"

export const mainBoxStyle = {
    display: "grid",
    gridTemplateColumns: "1fr 4fr"     
}

export const profileBoxStyle = { 
    "&": {
        display: "flex",
        flexDirection: "column",
        alignItems: "left",
        width: 300
    },
}

export const avatarStyle = { 
    width: 240, 
    height: 240,
    fontSize: 100,
    mb: 2,
    ml: 3,
    bgcolor: deepOrange[500]
}

export const userInfoBlocks = {
    display: 'flex',
    flexWrap: 'wrap',
    flexDirection: 'column',
    ml: 2
}

export const userInfoSubSetBlock = {
    mb: 3,
    "*": {
        mr: 1
    }
}