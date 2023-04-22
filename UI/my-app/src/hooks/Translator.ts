import { useTranslation } from "react-i18next"

export const useCult = () => {
    const { t, i18n } = useTranslation('common');

    const translator = (key: string) => {
        return t(key);
    }

    return { translator, i18n };
}