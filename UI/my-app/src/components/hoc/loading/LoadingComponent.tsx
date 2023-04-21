import { CircularProgress } from '@mui/material';
import LinearProgress from '@mui/material/LinearProgress';

interface Props {
  type?: "circle" | "inline";
}

export default function LoadingComponent({type = 'inline'}: Props) {
  switch(type) {
    case 'inline': 
      return <LinearProgress/>;
    case 'circle':
      return <CircularProgress/>;
    default:
       return <LinearProgress/>;
  }
}