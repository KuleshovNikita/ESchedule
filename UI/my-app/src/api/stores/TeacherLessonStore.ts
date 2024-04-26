import { makeAutoObservable } from "mobx";
import { TeachersLessonsCreateModel, TeachersLessonsModel } from "../../models/ManyToMany";
import { agent } from "../agent";

export default class TeacherLessonStore {
    client = agent.TeacherLesson;
    items: TeachersLessonsModel | null = null;

    constructor() {
        makeAutoObservable(this);
    }

    createItems = async (settings: TeachersLessonsCreateModel) => 
        await this.client.createItems(settings);

    removeItems = async (id: string) => 
        await this.client.removeItems(id);

    getItems = async (id: string) => 
        await this.client.getItems(id);
}