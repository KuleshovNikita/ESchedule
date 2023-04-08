import BaseEntity from "./Base";

export interface GroupCreateModel {
    title: string;
    maxLessonsCountPerDay: number;
    tenantId: string;
} 

export interface GroupUpdateModel {
    title: string | null;
    maxLessonsCountPerDay: number | null;
    tenantId: string | null;
} 

export interface GroupModel extends BaseEntity {
    title: string;
    maxLessonsCountPerDay: number;
    tenantId: string;
} 