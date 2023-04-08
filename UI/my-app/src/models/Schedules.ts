import BaseEntity from "./Base";

export interface RuleInputModel {
    ruleName: string;
    jsonBody: string;
}

export interface RuleModel extends BaseEntity {
    ruleName: string;
    jsonBody: string;
    tenantId: string;
}

export interface ScheduleModel extends BaseEntity {
    startTime: Date;
    endTime: Date;
    dayOfWeek: DayOfWeek;
    studyGroupId: string;
    teacherId: string;
    lessonId: string;
    tenantId: string;
}

export enum DayOfWeek {
    Sunday = 0,
    Monday = 1,
    Tuesday = 2,
    Wednesday = 3,
    Thursday = 4,
    Friday = 5,
    Saturday = 6
}