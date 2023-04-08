import BaseEntity from "./Base";

export interface LessonCreateModel {
    title: string;
    tenantId: string;
}

export interface LessonUpdateModel {
    title: string | null;
    tenantId: string | null;
}

export interface LessonModel extends BaseEntity {
    title: string;
    tenantId: string;
}