import { makeAutoObservable, runInAction } from "mobx";
import { RequestTenantAccessModel, TenantCreateModel, TenantModel, TenantUpdateModel } from "../../models/Tenants";
import { agent } from "../agent";
import { UserModel } from "../../models/Users";
import { GroupModel } from "../../models/Groups";
import { CacheDisposable } from "./StoresManager";

export default class TenantStore implements CacheDisposable {
    client = agent.Tenant;
    tenant: TenantModel | null = null;
    tenantGroups: GroupModel[] = [];
    accessRequests: UserModel[] | null = null;
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
        
    updateTenant = async (tenant: TenantUpdateModel) => {
        const updatedTenant = await this.client.updateTenant(tenant); 
        this.tenant = updatedTenant;

        return updatedTenant;
    }

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

    acceptAccessRequest = async (userId: string) => 
        await this.client.acceptAccessRequest(userId)
            .then(() => this.accessRequests!.filter(x => x.id !== userId))
            .then(res => this.accessRequests = res);

    declineAccessRequest = async (userId: string) => 
        await this.client.declineAccessRequest(userId)
            .then(() => this.accessRequests!.filter(x => x.id !== userId))
            .then(res => this.accessRequests = res);

    sendTenantAccessRequest = async (request: RequestTenantAccessModel) => 
        await this.client.sendTenantAccessRequest(request);

    getTenantAccessRequests = async () => {
        if(this.accessRequests && this.accessRequests !== null) {
            return this.accessRequests;
        }

        const response = await this.client.getTenantAccessRequests();

        if(response) {
            runInAction(() => {
                this.accessRequests = response;
            })
        }

        return response;
    }
        

    clearCache = () => {
        this.tenant = null;
        this.tenantGroups = [];
        this.tenantTeachers = [];
        this.accessRequests = null;
    }
}