import { makeAutoObservable } from "mobx";
import { TenantSettingsCreateModel, TenantSettingsModel, TenantSettingsUpdateModel } from "../../models/Tenants";
import { agent } from "../agent";
import BaseStore from "./BaseStore";

export default class TenantSettingsStore extends BaseStore {
    client = agent.TenantSettings;
    settings: TenantSettingsModel | null = null;

    constructor() {
        super();
        makeAutoObservable(this);
    }

    createTenantSettings = async (settings: TenantSettingsCreateModel) => 
        await this.simpleRequest(async () => await this.client.createTenantSettings(settings));

    updateTenantSettings = async (settings: TenantSettingsUpdateModel) =>
        await this.simpleRequest(async () => await this.client.updateTenantSettings(settings));

    removeTenantSettings = async (id: string) =>
        await this.simpleRequest(async () => await this.client.removeTenantSettings(id));

    getTenantSettings = async (tenantId: string) => {
        const response = await this.client.getTenantSettings(tenantId);

        this.handleErrors(response);

        return response.value;
    }
}