import { UserModel } from "./Users";

export interface BaseRule {
    Id: string;
    RuleName: string;
}

export interface TeacherBusyDayRule extends BaseRule {
    Actor: UserModel,
    DayOfWeek: string
}