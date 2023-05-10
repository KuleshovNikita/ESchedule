import Accordion from '@mui/material/Accordion';
import AccordionSummary from '@mui/material/AccordionSummary';
import AccordionDetails from '@mui/material/AccordionDetails';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import { Typography } from '@mui/material';

interface Props {
    children: any,
    title: string
}

const style = {
    backgroundColor: 'rgb(250, 222, 182)'
}

export default function CustomAccordion({children, title}: Props) {
    return(
    <Accordion>
        <AccordionSummary
            expandIcon={<ExpandMoreIcon />}
            aria-controls="panel1a-content"
            sx={style}
            id="panel1a-header"
        >
            <Typography variant='h4'>{title}</Typography>
        </AccordionSummary>
        <AccordionDetails sx={style}>
            {children}
        </AccordionDetails>
    </Accordion>
    );
}