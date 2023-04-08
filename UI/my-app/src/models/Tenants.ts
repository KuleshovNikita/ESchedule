import BaseEntity from "./Base";

export interface TenantCreateModel {
    tenantName: string;
}

export interface TenantUpdateModel {
    tenantName: string | null;
}

export interface TenantModel extends BaseEntity {
    tenantName: string;
}

export interface TenantSettingsCreateModel {
    studyDayStartTime: Date;
    lessonDurationTime: Date;
    breaksDurationTime: Date;
    tenantId: string;
    creatorId: string
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

