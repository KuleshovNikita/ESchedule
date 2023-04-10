import CircularProgress from '@mui/material/CircularProgress';
import Box from '@mui/material/Box';
import { loadingSpinnerStyle } from './LoadingComponentStyles';

export default function LoadingComponent() {
  return (
    <Box sx={loadingSpinnerStyle}>
      <CircularProgress />
    </Box>
  );
}