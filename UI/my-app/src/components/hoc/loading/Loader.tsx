import LinearProgress from '@mui/material/LinearProgress';
import { ReactNode, useEffect, useState } from 'react';
import { loaderEventName } from '../../../utils/Utils';
import CircularProgress from '@mui/material/CircularProgress';

type LoaderType = 'spin' | 'line';

type Props = {
  replace?: boolean;
  children?: ReactNode;
  type?: LoaderType;
}

const Loader = ({type = 'line', replace = false, children}: Props) => {
  const [state, setState] = useState(false);

  const getLoader = () => {
    switch (type) {
      case 'line':
        return <LinearProgress/>
      case 'spin':
        return <CircularProgress/>
      default:
        return <LinearProgress/>
    }
  }

  const handler = (e: any) => {
    setState(e.detail.state);   
  };
  
  useEffect(() => {
    window.addEventListener(loaderEventName, handler);

    return () => window.removeEventListener(loaderEventName, handler)
  }, [])

  return (
      state 
    ?
      <>
        <LinearProgress/>
        {
          replace
            ? <></>
            : children
          
        }
      </>
    :
    <>
      {children}
    </>
  );
}

export default Loader;