import { makeAutoObservable } from "mobx";
import { TenantCreateModel, TenantModel, TenantUpdateModel } from "../../models/Tenants";
import { agent } from "../agent";
import { UserModel } from "../../models/Users";
import { GroupModel } from "../../models/Groups";
import { CacheDisposable } from "./StoresManager";

export default class TenantStore implements CacheDisposable {
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

    clearCache = () => {
        this.tenant = null;
        this.tenantGroups = [];
        this.tenantTeachers = [];
    }
}