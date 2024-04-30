import BaseEntity from "./Base";
import { GroupModel } from "./Groups";
import { LessonModel } from "./Lessons";
import { TenantModel } from "./Tenants";
import { UserModel } from "./Users";

export interface RuleInputModel {
    ruleName: string;
    ruleJson: string;
}

export interface RuleModel extends BaseEntity {
    ruleName: string;
    ruleJson: string;
    tenantId: string;
}

export interface ScheduleModel extends BaseEntity {
    startTime: Date;
    endTime: Date;
    dayOfWeek: DayOfWeek;
    studyGroupId: string;
    studyGroup: GroupModel;
    teacherId: string;
    teacher: UserModel;
    lessonId: string;
    lesson: LessonModel;
    tenantId: string;
    tenant: TenantModel;
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