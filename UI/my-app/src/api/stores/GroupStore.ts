import { makeAutoObservable } from "mobx";
import { GroupCreateModel, GroupModel, GroupUpdateModel } from "../../models/Groups";
import { agent } from "../agent";
import BaseStore from "./BaseStore";

export default class GroupStore extends BaseStore {
    client = agent.Group;
    group: GroupModel | null = null;

    constructor() {
        super();
        makeAutoObservable(this);
    }

    createGroup = async (group: GroupCreateModel) => 
        await this.simpleRequest(async () => await this.client.createGroup(group));
    
    updateGroup = async (group: GroupUpdateModel) => 
        await this.simpleRequest(async () => await this.client.updateGroup(group));

    removeGroup = async (id: string) => 
        await this.simpleRequest(async () => await this.client.removeGroup(id));

    getGroup = async (id: string) => {
        const response = await this.client.getGroup(id);

        this.handleErrors(response);

        return response.value;
    }
}