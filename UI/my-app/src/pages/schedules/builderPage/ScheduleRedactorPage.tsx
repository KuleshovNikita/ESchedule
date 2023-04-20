import { useCult } from "../../../hooks/Translator";

export default function ScheduleRedactorPage() {
    const { translator } = useCult();

    return (<>{translator('schedule-builder-placeholder')}</>);
}