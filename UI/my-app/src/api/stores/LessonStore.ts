import { makeAutoObservable } from "mobx";
import { LessonCreateModel, LessonModel, LessonUpdateModel } from "../../models/Lessons";
import { agent } from "../agent";
import { CacheDisposable } from "./StoresManager";

export default class LessonStore implements CacheDisposable {
    lessons: LessonModel[] | null = null;
    client = agent.Lesson;

    constructor() {
        makeAutoObservable(this);
    }

    createLesson = async (lesson: LessonCreateModel) =>
        await this.client.createLesson(lesson)
            .then((item) => this.lessons?.push(item));

    updateLesson = async (lesson: LessonUpdateModel) => {
        const updatedLesson = await this.client.updateLesson(lesson);
        this.lessons = this.lessons!.map((val: LessonModel) => { 
            if(val.id === updatedLesson.id) {
                val = updatedLesson;
            }

            return val;
        });
        
        return updatedLesson;
    }

    removeLesson = async (id: string) => 
        await this.client.removeLesson(id);

    getLesson = async (id: string) => 
        await this.client.getLesson(id);

    getLessons = async () => {
        if(this.lessons) {
            return this.lessons;
        } 

        const res = await this.client.getLessons();
        this.lessons = res;

        return res;
    }

    removeLessons = async (lessons: string[]) =>
        await this.client.removeLessons(lessons)
            .then(() => this.lessons!.filter(l => !lessons.includes(l.id)))
            .then(res => this.lessons = res);

    clearCache = () => {
        this.lessons = null;
    }
}