import { makeAutoObservable } from "mobx";
import { GroupCreateModel, GroupModel, GroupUpdateModel } from "../../models/Groups";
import { agent } from "../agent";

export default class GroupStore {
    client = agent.Group;
    group: GroupModel | null = null;

    constructor() {
        makeAutoObservable(this);
    }

    createGroup = async (group: GroupCreateModel) => 
        await this.client.createGroup(group);
    
    updateGroup = async (group: GroupUpdateModel) => 
        await this.client.updateGroup(group);

    removeGroup = async (id: string) => 
        await this.client.removeGroup(id);

    getGroup = async (id: string) => 
        await this.client.getGroup(id);
}