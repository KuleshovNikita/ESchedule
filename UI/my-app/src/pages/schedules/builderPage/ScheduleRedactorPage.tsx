import { useTranslation } from "react-i18next";

export default function ScheduleRedactorPage() {
    const { t } = useTranslation('common');

    return (<>{t('ScheduleBuilderPlaceholder')}</>);
}