import Button from "@mui/material/Button";
import Typography from "@mui/material/Typography";
import EIcon, { IconType } from "../../../components/wrappers/EIcon";
import { btnStyle, iconStyle } from "./TenantManagerStyles";
import { useNavigate } from "react-router-dom";
import { useCult } from "../../../hooks/useTranslator";

type TenantManagerButtonProps = {
    icon: IconType,
    text: string,
    route: string
}

export const TenantManagerButton = (props: TenantManagerButtonProps) => {
    const navigate = useNavigate();
    const { translator } = useCult();

    return (
        <Button sx={btnStyle} variant="contained" onClick={() => navigate(props.route)}>
            <EIcon type={props.icon} sx={iconStyle}/>
            <Typography>
                {translator(props.text)}
            </Typography>
        </Button>
    );
}