import BaseEntity from "./Base";
import { GroupModel } from "./Groups";
import { TenantModel } from "./Tenants";

export interface UserCreateModel extends UserLoginModel {
    name: string;
    lastName: string;
    fatherName: string;
    age: number;
    role: Role;
    tenantId: string | null;
}

export interface UserUpdateModel extends BaseEntity {
    name: string | null;
    lastName: string | null;
    fatherName: string | null;
    login: string | null;
    password: string | null;
    age: number | null;
    role: Role | null;
    groupId: string | null;
    tenantId: string | null;
}

export interface UserModel extends BaseEntity {
    name: string;
    lastName: string;
    fatherName: string;
    login: string;
    password: string;
    age: number;
    role: Role;
    isEmailConfirmed: boolean;
    groupId: string | null;
    group: GroupModel;
    tenantId: string | null;
    tenant: TenantModel;
}

export interface UserLoginModel {
    login: string;
    password: string;
}

export enum Role {
    Pupil = 0,
    Teacher = 1
}