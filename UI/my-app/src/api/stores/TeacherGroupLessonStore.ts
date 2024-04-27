import { makeAutoObservable } from "mobx";
import { TeachersGroupsLessonsCreateModel, TeachersGroupsLessonsModel } from "../../models/ManyToMany";
import { agent } from "../agent";

export default class TeacherGroupLessonStore {
    client = agent.TeacherGroupLesson;
    items: TeachersGroupsLessonsModel | null = null;

    constructor() {
        makeAutoObservable(this);
    }

    createItems = async (settings: TeachersGroupsLessonsCreateModel) => 
        await this.client.createItems(settings);

    removeItems = async (id: string) => 
        await this.client.removeItems(id);

    getItems = async (id: string) => 
        await this.client.getItems(id);
}