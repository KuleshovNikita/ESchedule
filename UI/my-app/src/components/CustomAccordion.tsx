import Accordion from '@mui/material/Accordion';
import AccordionSummary from '@mui/material/AccordionSummary';
import AccordionDetails from '@mui/material/AccordionDetails';
import { Typography } from '@mui/material';
import EIcon from './wrappers/EIcon';

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
            expandIcon={<EIcon type='expand'/>}
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