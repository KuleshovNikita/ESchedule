import { makeAutoObservable } from "mobx";
import { RuleInputModel, RuleModel, ScheduleModel } from "../../models/Schedules";
import { agent } from "../agent";

export default class ScheduleStore {
    client = agent.Schedule;
    schedules: ScheduleModel[] | null = null;
    rules: RuleModel[] | null = null;

    constructor() {
        makeAutoObservable(this);
    }

    buildSchedule = async (tenantId: string) => 
        await this.client.buildSchedule(tenantId); 

    getScheduleForTenant = async (tenantId: string) => {
        const response = await this.client.getScheduleForGroup(tenantId);

        if(response) {
            this.schedules = this.parseDate(response);
        }

        return response;
    }
 
    getScheduleForGroup = async (groupId: string) => {
        const response = await this.client.getScheduleForGroup(groupId);

        if(response) {
            this.schedules = this.parseDate(response);
        }

        return response;
    }

    getScheduleRules = async (tenantId: string) => {
        if(this.rules) {
            return this.rules;
        }

        const response = await this.client.getScheduleRules(tenantId);

        if(response) {
            this.rules = response;
        }

        return response;
    }

    getScheduleForTeacher = async (teacherId: string) => {
        if(this.schedules) {
            return this.schedules;
        }

        const response = await this.client.getScheduleForTeacher(teacherId);

        if(response) {
            this.schedules = this.parseDate(response);
        }

        return response;
    }

    getScheduleItem = async (id: string) => 
        await this.client.getScheduleItem(id)
            .then(res => this.parseDate([res])[0]);

    removeSchedule = async (tenantId: string) =>
        await this.client.removeSchedule(tenantId); 

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
        await this.client.createRule(rule); 

    removeRule = async (ruleId: string) => 
        await this.client.removeRule(ruleId); 
}
