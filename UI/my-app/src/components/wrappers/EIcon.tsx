import AddCircle from "@mui/icons-material/AddCircle";
import { buttonImageIconStyle } from "../../styles/ButtonStyles";
import ExpandMore from "@mui/icons-material/ExpandMore";
import Delete from "@mui/icons-material/Delete";
import Save from "@mui/icons-material/Save";
import Logout from "@mui/icons-material/Logout";
import CalendarMonth from "@mui/icons-material/CalendarMonth";
import ManageAccounts from "@mui/icons-material/ManageAccounts";
import Construction from "@mui/icons-material/Construction";
import Preview from "@mui/icons-material/Preview";
import Close from "@mui/icons-material/Close";
import Edit from "@mui/icons-material/Edit";
import Cancel from "@mui/icons-material/Cancel";
import PersonAdd from '@mui/icons-material/PersonAdd';
import { SxProps, Theme } from "@mui/material";
import MapsUgcIcon from '@mui/icons-material/MapsUgc';
import PlaylistAddIcon from '@mui/icons-material/PlaylistAdd';

export type IconType = 
      'add' 
    | 'remove' 
    | 'delete'
    | 'cancel' 
    | 'edit' 
    | 'expand'
    | 'save'
    | 'quit'
    | 'calendar'
    | 'manage accounts'
    | 'build'
    | 'preview'
    | 'close'
    | 'person add'
    | 'request'
    | 'list'

type Props = {
    type: IconType;
    sx?: SxProps<Theme> | undefined;
    fontSize?: string;
} & any 

const EIcon = (props: Props) => {
    const style = {...props.sx, ...buttonImageIconStyle};

    switch (props.type)
    {
        case 'add': return <AddCircle sx={style} {...props}/>
        case 'remove': 
        case 'delete': return <Delete sx={style} {...props}/>
        case 'cancel': return <Cancel sx={style} {...props}/>
        case 'edit': return <Edit sx={style} {...props}/>
        case 'expand': return <ExpandMore sx={style} {...props}/>
        case 'save': return <Save sx={style} {...props}/>
        case 'quit': return <Logout sx={style} {...props}/>
        case 'calendar': return <CalendarMonth sx={style} {...props}/>
        case 'manage accounts': return <ManageAccounts sx={style} {...props}/>
        case 'build': return <Construction sx={style} {...props}/>
        case 'preview': return <Preview sx={style} {...props}/>
        case 'close': return <Close sx={style} {...props}/>
        case 'person add': return <PersonAdd sx={style} {...props}/>
        case 'request': return <MapsUgcIcon sx={style} {...props}/>
        case 'list': return <PlaylistAddIcon sx={style} {...props}/>
        default:
            return <>No icon</>
    }
         
}

export default EIcon; 