import { makeAutoObservable } from "mobx";
import { GroupsLessonsCreateModel, GroupsLessonsModel } from "../../models/ManyToMany";
import { agent } from "../agent";
import { CacheDisposable } from "./StoresManager";

export default class GroupLessonStore implements CacheDisposable {
    client = agent.GroupLesson;
    items: GroupsLessonsModel | null = null;

    constructor() {
        makeAutoObservable(this);
    }

    createItems = async (settings: GroupsLessonsCreateModel) => 
        await this.client.createItems(settings);

    removeItems = async (id: string) => 
        await this.client.removeItems(id);

    getItems = async (id: string) =>
        await this.client.getItems(id);

    clearCache = () => {
        this.items = null;
    }
}