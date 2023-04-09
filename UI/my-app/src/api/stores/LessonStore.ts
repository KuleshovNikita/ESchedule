import { makeAutoObservable } from "mobx";
import { LessonCreateModel, LessonModel, LessonUpdateModel } from "../../models/Lessons";
import { agent } from "../agent";
import BaseStore from "./BaseStore";

export default class LessonStore {
    base: BaseStore = new BaseStore();
    lesson: LessonModel | null = null;
    client = agent.Lesson;

    constructor() {
        makeAutoObservable(this);
    }

    createLesson = async (lesson: LessonCreateModel) =>
        await this.base.simpleRequest(async () => await this.client.createLesson(lesson));

    updateLesson = async (lesson: LessonUpdateModel) => 
        await this.base.simpleRequest(async () => await this.client.updateLesson(lesson));

    removeLesson = async (id: string) => 
        await this.base.simpleRequest(async () => await this.client.removeLesson(id));

    getLesson = async (id: string) => {
        const response = await this.client.getLesson(id);

        this.base.handleErrors(response);

        return response.value;
    }
}