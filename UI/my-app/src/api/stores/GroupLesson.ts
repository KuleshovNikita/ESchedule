import { makeAutoObservable } from "mobx";
import { GroupsLessonsCreateModel, GroupsLessonsModel } from "../../models/ManyToMany";
import { agent } from "../agent";
import BaseStore from "./BaseStore";

export default class GroupLessonStore extends BaseStore {
    client = agent.GroupLesson;
    items: GroupsLessonsModel | null = null;

    constructor() {
        super();
        makeAutoObservable(this);
    }

    createItems = async (settings: GroupsLessonsCreateModel) => 
        await this.simpleRequest(async () => await this.client.createItems(settings));

    removeItems = async (id: string) => 
        await this.simpleRequest(async () => await this.client.removeItems(id));

    getItems = async (id: string) => {
        const response = await this.client.getItems(id);

        this.handleErrors(response);

        return response.value;
    }
}