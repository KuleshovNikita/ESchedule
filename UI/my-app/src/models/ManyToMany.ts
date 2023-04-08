import BaseEntity from "./Base";

export interface GroupsLessonsModel extends BaseEntity {
    lessonId: string;
    studyGroupId: string;
}

export interface TeachersLessonsModel extends BaseEntity {
    lessonId: string;
    teacherId: string;
}

export interface TeachersGroupsLessonsModel extends BaseEntity {
    lessonId: string;
    studyGroupId: string;
    teacherId: string;
}

export interface GroupsLessonsCreateModel {
    lessonId: string;
    studyGroupId: string;
}

export interface TeachersLessonsCreateModel {
    lessonId: string;
    teacherId: string;
}

export interface TeachersGroupsLessonsCreateModel {
    lessonId: string;
    studyGroupId: string;
    teacherId: string;
}