import { makeAutoObservable } from "mobx";
import { GroupCreateModel, GroupModel, GroupUpdateModel } from "../../models/Groups";
import { agent } from "../agent";
import { CacheDisposable } from "./StoresManager";

export default class GroupStore implements CacheDisposable {
    client = agent.Group;
    groups: GroupModel[] | null = null;

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

    getGroups = async () => {
        if(this.groups) {
            return this.groups;
        }

        var res = await this.client.getGroups();

        this.groups = res;

        return res;
    }

    clearCache = () => {
        this.groups = null;
    }
        
}