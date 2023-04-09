import { makeAutoObservable } from "mobx";
import { TenantCreateModel, TenantModel, TenantUpdateModel } from "../../models/Tenants";
import { agent } from "../agent";
import BaseStore from "./BaseStore";

export default class TenantStore {
    base: BaseStore = new BaseStore();
    client = agent.Tenant;
    tenant: TenantModel | null = null;

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
        const response = await this.client.getTenant(id);

        this.base.handleErrors(response);

        return response.value;
    }
}