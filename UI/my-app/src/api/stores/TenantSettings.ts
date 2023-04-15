import { makeAutoObservable } from "mobx";
import { ScheduleStartEndTime, TenantSettingsCreateModel, TenantSettingsModel, TenantSettingsUpdateModel } from "../../models/Tenants";
import { agent } from "../agent";
import BaseStore from "./BaseStore";

export default class TenantSettingsStore {
    base: BaseStore = new BaseStore();
    client = agent.TenantSettings;
    settings: TenantSettingsModel | null = null;
    timeTableList: ScheduleStartEndTime[] | null = null;

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

    getTenantScheduleTimes = async (tenantId: string) => {
        if(this.timeTableList) {
            return this.timeTableList;
        }

        const response = await this.client.getTenantScheduleTimes(tenantId);

        this.base.handleErrors(response);

        if(response.isSuccessful) {
            this.timeTableList = this.parseDate(response.value);
        }

        return response.value;
    }

    parseDate = (schedules: ScheduleStartEndTime[]) => {
        schedules.forEach(el => {
            const start = el.startTime.toString().split(':');
            el.startTime = new Date(0, 0, 0, Number(start[0]), Number(start[1]), Number(start[2]));

            const end = el.endTime.toString().split(':');
            el.endTime = new Date(0, 0, 0, Number(end[0]), Number(end[1]), Number(end[2]));
        });       

        return schedules;
    }
}