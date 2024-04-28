import { makeAutoObservable } from "mobx";
import { TenantCreateModel, TenantModel, TenantUpdateModel } from "../../models/Tenants";
import { agent } from "../agent";
import { UserModel } from "../../models/Users";
import { GroupModel } from "../../models/Groups";

export default class TenantStore {
    client = agent.Tenant;
    tenant: TenantModel | null = null;
    tenantGroups: GroupModel[] = [];
    tenantTeachers: UserModel[] = [];

    constructor() {
        makeAutoObservable(this);
    }

    createTenant = async (tenant: TenantCreateModel) => {
        tenant.settings.breaksDurationTime += ":00";
        tenant.settings.lessonDurationTime += ":00";
        tenant.settings.studyDayStartTime += ":00";

        return await this.client.createTenant(tenant); 
    }
        
 
    updateTenant = async (tenant: TenantUpdateModel) => 
        await this.client.updateTenant(tenant); 

    removeTenant = async (id: string) => 
        await this.client.removeTenant(id); 

    getTenant = async (id: string) => {
        if(this.tenant && this.tenant.id === id) {
            return this.tenant;
        }

        const response = await this.client.getTenant(id);

        if(response) {
            this.tenant = response;
        }

        return response;
    }

    getTeachers = async (id: string) => {
        const response = await this.client.getTeachers(id);

        if(response) {
            this.tenantTeachers = response;
        }

        return response;
    }

    getGroups = async (tenantId: string) => {
        const response = await this.client.getGroups(tenantId);

        if(response) {
            this.tenantGroups = response;
        }

        return response;
    }

    getLessons = async (tenantId: string) => 
        await this.client.getLessons(tenantId);
}