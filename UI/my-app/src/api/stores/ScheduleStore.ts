import { makeAutoObservable } from "mobx";
import { RuleInputModel, ScheduleModel } from "../../models/Schedules";
import { agent } from "../agent";
import BaseStore from "./BaseStore";

export default class ScheduleStore extends BaseStore {
    client = agent.Schedule;
    schedule: ScheduleModel | null = null;

    constructor() {
        super();
        makeAutoObservable(this);
    }

    buildSchedule = async (tenantId: string, rules: RuleInputModel[]) => 
        await this.simpleRequest(async () => await this.client.buildSchedule(tenantId, rules)); 

    getScheduleForTenant = async (tenantId: string) => {
        const response = await this.client.getScheduleForGroup(tenantId);

        this.handleErrors(response);

        return response.value;
    }
 
    getScheduleForGroup = async (groupId: string) => {
        const response = await this.client.getScheduleForGroup(groupId);

        this.handleErrors(response);

        return response.value;
    }

    getScheduleRules = async (tenantId: string) => {
        const response = await this.client.getScheduleRules(tenantId);

        this.handleErrors(response);

        return response.value;
    }

    removeSchedule = async (tenantId: string) =>
        await this.simpleRequest(async () => await this.client.removeSchedule(tenantId)); 


}
