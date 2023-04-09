import { makeAutoObservable } from "mobx";
import { RuleInputModel, ScheduleModel } from "../../models/Schedules";
import { agent } from "../agent";
import BaseStore from "./BaseStore";

export default class ScheduleStore {
    base: BaseStore = new BaseStore();
    client = agent.Schedule;
    schedule: ScheduleModel | null = null;

    constructor() {
        makeAutoObservable(this);
    }

    buildSchedule = async (tenantId: string, rules: RuleInputModel[]) => 
        await this.base.simpleRequest(async () => await this.client.buildSchedule(tenantId, rules)); 

    getScheduleForTenant = async (tenantId: string) => {
        const response = await this.client.getScheduleForGroup(tenantId);

        this.base.handleErrors(response);

        return response.value;
    }
 
    getScheduleForGroup = async (groupId: string) => {
        const response = await this.client.getScheduleForGroup(groupId);

        this.base.handleErrors(response);

        return response.value;
    }

    getScheduleRules = async (tenantId: string) => {
        const response = await this.client.getScheduleRules(tenantId);

        this.base.handleErrors(response);

        return response.value;
    }

    removeSchedule = async (tenantId: string) =>
        await this.base.simpleRequest(async () => await this.client.removeSchedule(tenantId)); 


}
