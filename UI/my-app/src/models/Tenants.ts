import BaseEntity from "./Base";

export interface TenantCreateModel {
    name: string;
    settings: TenantSettingsCreateModel
}

export interface TenantUpdateModel {
    name: string | null;
}

export interface TenantModel extends BaseEntity {
    name: string;
}

export interface TenantSettingsCreateModel {
    studyDayStartTime: string;
    lessonDurationTime: string;
    breaksDurationTime: string;
}

export interface TenantSettingsUpdateModel {
    studyDayStartTime: Date | null;
    lessonDurationTime: Date | null;
    breaksDurationTime: Date | null;
    tenantId: string | null;
    creatorId: string | null;
}

export interface TenantSettingsModel extends BaseEntity {
    studyDayStartTime: Date;
    lessonDurationTime: Date;
    breaksDurationTime: Date;
    tenantId: string;
}

export interface ScheduleStartEndTime {
    startTime: Date;
    endTime: Date;
}

