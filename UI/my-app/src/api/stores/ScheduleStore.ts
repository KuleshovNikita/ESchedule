import { makeAutoObservable } from "mobx";
import { RuleInputModel, RuleModel, ScheduleModel } from "../../models/Schedules";
import { agent } from "../agent";
import BaseStore from "./BaseStore";

export default class ScheduleStore {
    base: BaseStore = new BaseStore();
    client = agent.Schedule;
    schedules: ScheduleModel[] | null = null;
    rules: RuleModel[] | null = null;

    constructor() {
        makeAutoObservable(this);
    }

    buildSchedule = async (tenantId: string, rules: RuleInputModel[]) => 
        await this.base.simpleRequest(async () => await this.client.buildSchedule(tenantId, rules)); 

    getScheduleForTenant = async (tenantId: string) => {
        const response = await this.client.getScheduleForGroup(tenantId);

        this.base.handleErrors(response);

        if(response.isSuccessful) {
            this.schedules = this.parseDate(response.value);
        }

        return response.value;
    }
 
    getScheduleForGroup = async (groupId: string) => {
        const response = await this.client.getScheduleForGroup(groupId);

        this.base.handleErrors(response);

        if(response.isSuccessful) {
            this.schedules = this.parseDate(response.value);
        }

        return response.value;
    }

    getScheduleRules = async (tenantId: string) => {
        if(this.rules) {
            return this.rules;
        }

        const response = await this.client.getScheduleRules(tenantId);

        this.base.handleErrors(response);

        if(response.isSuccessful) {
            this.rules = response.value;
        }

        return response.value;
    }

    getScheduleForTeacher = async (teacherId: string) => {
        if(this.schedules) {
            return this.schedules;
        }

        const response = await this.client.getScheduleForTeacher(teacherId);

        this.base.handleErrors(response);

        if(response.isSuccessful) {
            this.schedules = this.parseDate(response.value);
        }

        return response.value;
    }

    removeSchedule = async (tenantId: string) =>
        await this.base.simpleRequest(async () => await this.client.removeSchedule(tenantId)); 

    parseDate = (schedules: ScheduleModel[]) => {
        schedules.forEach(el => {
            const start = el.startTime.toString().split(':');
            el.startTime = new Date(0, 0, 0, Number(start[0]), Number(start[1]), Number(start[2]));

            const end = el.endTime.toString().split(':');
            el.endTime = new Date(0, 0, 0, Number(end[0]), Number(end[1]), Number(end[2]));
        });

        return schedules;
    }

    createRule = async (rule: RuleInputModel) => 
        await this.base.simpleRequest(async () => await this.client.createRule(rule)); 
}
