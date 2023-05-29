import { makeAutoObservable } from "mobx";
import { TenantCreateModel, TenantModel, TenantUpdateModel } from "../../models/Tenants";
import { agent } from "../agent";
import BaseStore from "./BaseStore";
import { UserModel } from "../../models/Users";
import { GroupModel } from "../../models/Groups";

export default class TenantStore {
    base: BaseStore = new BaseStore();
    client = agent.Tenant;
    tenant: TenantModel | null = null;
    tenantGroups: GroupModel[] = [];
    tenantTeachers: UserModel[] = [];

    constructor() {
        makeAutoObservable(this);
    }

    createTenant = async (tenant: TenantCreateModel) => 
        await this.base.simpleRequest(async () => await this.client.createTenant(tenant)); 
 
    updateTenant = async (tenant: TenantUpdateModel) => 
        await this.base.simpleRequest(async () => await this.client.updateTenant(tenant)); 

    removeTenant = async (id: string) => 
        await this.base.simpleRequest(async () => await this.client.removeTenant(id)); 

    getTenant = async (id: string) => {
        if(this.tenant && this.tenant.id === id) {
            return this.tenant;
        }

        const response = await this.client.getTenant(id);

        this.base.handleErrors(response);

        if(response.isSuccessful) {
            this.tenant = response.value;
        }

        return response.value;
    }

    getTeachers = async (id: string) => {
        const response = await this.client.getTeachers(id);

        this.base.handleErrors(response);

        if(response.isSuccessful) {
            this.tenantTeachers = response.value;
        }

        return response.value;
    }

    getGroups = async (tenantId: string) => {
        const response = await this.client.getGroups(tenantId);

        this.base.handleErrors(response);

        if(response.isSuccessful) {
            this.tenantGroups = response.value;
        }

        return response.value;
    }

    getLessons = async (tenantId: string) => {
        const response = await this.client.getLessons(tenantId);

        this.base.handleErrors(response);

        return response.value;
    }
}