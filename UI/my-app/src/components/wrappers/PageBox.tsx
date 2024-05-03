import Box from "@mui/material/Box";

const screenBoxStyle = {
    height: 'fill',
    pb: '24px'
}

const PageBox = ({children}: any) => {   
    return(
        <Box sx={screenBoxStyle}>
            {children}
        </Box>
    )
}

export default PageBox;