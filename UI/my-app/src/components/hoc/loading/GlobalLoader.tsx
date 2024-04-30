import LinearProgress from '@mui/material/LinearProgress';
import { useEffect, useState } from 'react';

export default function GlobalLader() {
  const [state, setState] = useState(false);

  const handler = (e: any) => { 
    setState(e.detail.state);   
  };
  
  useEffect(() => {
    window.addEventListener('showLoader', handler);

    return () => window.removeEventListener('showLoader', handler)
  }, [])

  return (
    state ? <LinearProgress/> : <></>
  );
}