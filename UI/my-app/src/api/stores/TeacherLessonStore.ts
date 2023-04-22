import { makeAutoObservable } from "mobx";
import { TeachersLessonsCreateModel, TeachersLessonsModel } from "../../models/ManyToMany";
import { agent } from "../agent";
import BaseStore from "./BaseStore";

export default class TeacherLessonStore {
    base: BaseStore = new BaseStore();
    client = agent.TeacherLesson;
    items: TeachersLessonsModel | null = null;

    constructor() {
        makeAutoObservable(this);
    }

    createItems = async (settings: TeachersLessonsCreateModel) => 
        await this.base.simpleRequest(async () => await this.client.createItems(settings));

    removeItems = async (id: string) => 
        await this.base.simpleRequest(async () => await this.client.removeItems(id));

    getItems = async (id: string) => {
        const response = await this.client.getItems(id);

        this.base.handleErrors(response);

        return response.value;
    }
}