import Box from "@mui/material/Box";

const screenBoxStyle = {
    height: '100%'
}

const PageBox = ({children}: any) => {   
    return(
        <Box sx={screenBoxStyle}>
            {children}
        </Box>
    )
}

export default PageBox;