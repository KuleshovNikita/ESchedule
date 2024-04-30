import { makeAutoObservable } from "mobx";
import { ScheduleStartEndTime, TenantSettingsCreateModel, TenantSettingsModel, TenantSettingsUpdateModel } from "../../models/Tenants";
import { agent } from "../agent";

export default class TenantSettingsStore {
    client = agent.TenantSettings;
    settings: TenantSettingsModel | null = null;
    timeTableList: ScheduleStartEndTime[] | null = null;

    constructor() {
        makeAutoObservable(this);
    }

    createTenantSettings = async (settings: TenantSettingsCreateModel) => 
        await this.client.createTenantSettings(settings);

    updateTenantSettings = async (settings: TenantSettingsUpdateModel) =>
        await this.client.updateTenantSettings(settings);

    removeTenantSettings = async (id: string) =>
        await this.client.removeTenantSettings(id);

    getTenantSettings = async (tenantId: string) =>
        await this.client.getTenantSettings(tenantId);

    getTenantScheduleTimes = async () => {
        if(this.timeTableList) {
            return this.timeTableList;
        }

        const response = await this.client.getTenantScheduleTimes();

        if(response) {
            this.timeTableList = this.parseDate(response);
        }

        return response;
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