import { makeAutoObservable } from "mobx";
import { GroupCreateModel, GroupModel, GroupUpdateModel } from "../../models/Groups";
import { agent } from "../agent";
import BaseStore from "./BaseStore";

export default class GroupStore {
    base: BaseStore = new BaseStore();
    client = agent.Group;
    group: GroupModel | null = null;

    constructor() {
        makeAutoObservable(this);
    }

    createGroup = async (group: GroupCreateModel) => 
        await this.base.simpleRequest(async () => await this.client.createGroup(group));
    
    updateGroup = async (group: GroupUpdateModel) => 
        await this.base.simpleRequest(async () => await this.client.updateGroup(group));

    removeGroup = async (id: string) => 
        await this.base.simpleRequest(async () => await this.client.removeGroup(id));

    getGroup = async (id: string) => {
        const response = await this.client.getGroup(id);

        this.base.handleErrors(response);

        return response.value;
    }
}