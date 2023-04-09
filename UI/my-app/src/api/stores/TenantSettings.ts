import { makeAutoObservable } from "mobx";
import { TenantSettingsCreateModel, TenantSettingsModel, TenantSettingsUpdateModel } from "../../models/Tenants";
import { agent } from "../agent";
import BaseStore from "./BaseStore";

export default class TenantSettingsStore {
    base: BaseStore = new BaseStore();
    client = agent.TenantSettings;
    settings: TenantSettingsModel | null = null;

    constructor() {
        makeAutoObservable(this);
    }

    createTenantSettings = async (settings: TenantSettingsCreateModel) => 
        await this.base.simpleRequest(async () => await this.client.createTenantSettings(settings));

    updateTenantSettings = async (settings: TenantSettingsUpdateModel) =>
        await this.base.simpleRequest(async () => await this.client.updateTenantSettings(settings));

    removeTenantSettings = async (id: string) =>
        await this.base.simpleRequest(async () => await this.client.removeTenantSettings(id));

    getTenantSettings = async (tenantId: string) => {
        const response = await this.client.getTenantSettings(tenantId);

        this.base.handleErrors(response);

        return response.value;
    }
}