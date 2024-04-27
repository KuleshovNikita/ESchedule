import axios, { AxiosError, AxiosResponse } from "axios";
import { toast } from "react-toastify";
import { store } from "./stores/StoresManager";
import { redirect } from "react-router";
import { UserLoginModel, UserCreateModel, UserUpdateModel, UserModel } from "../models/Users";
import { GroupCreateModel, GroupModel, GroupUpdateModel } from "../models/Groups";
import { LessonCreateModel, LessonModel, LessonUpdateModel } from "../models/Lessons";
import { GroupsLessonsCreateModel, GroupsLessonsModel, TeachersGroupsLessonsCreateModel, TeachersGroupsLessonsModel, TeachersLessonsCreateModel, TeachersLessonsModel } from "../models/ManyToMany";
import { RuleInputModel, RuleModel, ScheduleModel } from "../models/Schedules";
import { ScheduleStartEndTime, TenantCreateModel, TenantModel, TenantSettingsCreateModel, TenantSettingsModel, TenantSettingsUpdateModel, TenantUpdateModel } from "../models/Tenants";
import i18n from "i18next";

interface ErrorResponse {
    detail: string
}

axios.defaults.baseURL = "https://localhost:20000/api";

axios.interceptors.request.use((config) => {
    const token = store.commonStore.token;

    if (token) {
        config.headers!.Authorization = `Bearer ${token}`;
    }

    return config;
});

axios.interceptors.response.use(async (response) => response,
    (error: AxiosError<ErrorResponse>) => {
        const { data, status, headers } = error.response!;
        switch (status) {
            case 400:
                toast.error('Bad request');
                break;
            case 401:
                if (headers['www-authenticate']?.startsWith('Bearer error="invalid_token"')) {
                    store.userStore.logout();
                    toast.error('The session has expired - please login again');
                }
                break;
            case 404:
                redirect("/notFound");
                break;              
            case 500:
                toast.error(i18n.t('server-errors.' + data.detail));             
                break;
        }

        return Promise.reject(error);
    }
);

const responseBody = <T>(response: AxiosResponse<T>) => response.data;

const requests = {
    get: <T>(url: string) => axios.get<T>(url).then(responseBody),
    post: <T>(url: string, body: {}) => axios.post<T>(url, body).then(responseBody),
    put: <T>(url: string, body: {}) => axios.put<T>(url, body).then(responseBody),
    delete: <T>(url: string) => axios.delete<T>(url).then(responseBody),
    patch: <T>(url: string) => axios.patch<T>(url).then(responseBody),
};

const Auth = {
    login: (body: UserLoginModel) => requests.post<string>("/authentication/login", body),
    register: (body: UserCreateModel) => requests.post("/authentication/register", body),
    getAuthenticatedUserInfo: () => requests.get<UserModel>("/authentication"),
    confirmEmail: (key: string) => requests.patch<string>(`/authentication/confirmEmail/${key}`)
}

const User = {
    updateUser: (body: UserUpdateModel) => requests.put("/user", body),
    getUser: (id: string) => requests.get<UserModel>(`/user/${id}`),
}

const Group = {
    createGroup: (body: GroupCreateModel) => requests.post(`/group`, body),
    updateGroup: (body: GroupUpdateModel) => requests.put(`/group`, body),
    getGroup: (id: string) => requests.get<GroupModel>(`/group/${id}`),
    removeGroup: (id: string) => requests.delete(`/group/${id}`),
}

const Lesson = {
    createLesson: (body: LessonCreateModel) => requests.post(`/lesson`, body),
    removeLessons: (body: string[], tenantId: string) => requests.put(`/lesson/many/${tenantId}`, body),
    updateLesson: (body: LessonUpdateModel) => requests.put(`/lesson`, body),
    getLesson: (id: string) => requests.get<LessonModel>(`/lesson/${id}`),
    removeLesson: (id: string) => requests.delete(`/lesson/${id}`),
}

const Schedule = {
    buildSchedule: (tenantId: string) => requests.post(`/schedule/${tenantId}`, {}), 
    //TODO ---> updateSchedule: (rules: RuleInputModel[]) => requests.post(`/schedule/${tenantId}`, rules), 
    getScheduleForTenant: (tenantId: string) => requests.get<ScheduleModel[]>(`/schedule/tenant/${tenantId}`), 
    getScheduleRules: (tenantId: string) => requests.get<RuleModel[]>(`/schedule/rules/${tenantId}`), 
    getScheduleForGroup: (groupId: string) => requests.get<ScheduleModel[]>(`/schedule/group/${groupId}`), 
    getScheduleForTeacher: (teacherId: string) => requests.get<ScheduleModel[]>(`/schedule/teacher/${teacherId}`), 
    removeSchedule: (tenantId: string) => requests.delete(`/schedule/${tenantId}`), 
    createRule: (rule: RuleInputModel) => requests.post(`/schedule/rule`, rule),
    removeRule: (ruleId: string) => requests.delete(`/schedule/rule/${ruleId}`),
    getScheduleItem: (id: string) => requests.get<ScheduleModel>(`/schedule/item/${id}`)
}

const Tenant = {
    createTenant: (body: TenantCreateModel) => requests.post(`/tenant`, body),
    updateTenant: (body: TenantUpdateModel) => requests.put(`/tenant`, body),
    getTenant: (id: string) => requests.get<TenantModel>(`/tenant/${id}`),
    removeTenant: (id: string) => requests.delete(`/tenant/${id}`),
    getTeachers: (id: string) => requests.get<UserModel[]>(`/tenant/teachers/${id}`),
    getGroups: (tenantId: string) => requests.get<GroupModel[]>(`/tenant/groups/${tenantId}`),
    getLessons: (tenantId: string) => requests.get<LessonModel[]>(`/tenant/lessons/${tenantId}`),
}

const TenantSettings = {
    createTenantSettings: (body: TenantSettingsCreateModel) => requests.post(`/tenantSettings`, body),
    updateTenantSettings: (body: TenantSettingsUpdateModel) => requests.put(`/tenantSettings`, body),
    getTenantSettings: (tenantId: string) => requests.get<TenantSettingsModel>(`/tenantSettings/${tenantId}`),
    getTenantScheduleTimes: (tenantId: string) => requests.get<ScheduleStartEndTime[]>(`/tenantSettings/time/${tenantId}`),
    removeTenantSettings: (tenantId: string) => requests.delete(`/tenantSettings/${tenantId}`),
}

const TeacherLesson = {
    createItems: (body: TeachersLessonsCreateModel) => requests.post(`/teachersLessons`, body),
    getItems: (id: string) => requests.get<TeachersLessonsModel>(`/teachersLessons/${id}`),
    removeItems: (id: string) => requests.delete(`/teachersLessons/${id}`),
}

const TeacherGroupLesson = {
    createItems: (body: TeachersGroupsLessonsCreateModel) => requests.post(`/teachersGroupsLessons`, body),
    getItems: (id: string) => requests.get<TeachersGroupsLessonsModel>(`/teachersGroupsLessons/${id}`),
    removeItems: (id: string) => requests.delete(`/teachersGroupsLessons/${id}`),
}

const GroupLesson = {
    createItems: (body: GroupsLessonsCreateModel) => requests.post(`/groupLessons`, body),
    getItems: (id: string) => requests.get<GroupsLessonsModel>(`/groupLessons/${id}`),
    removeItems: (id: string) => requests.delete(`/groupLessons/${id}`),
}

export const agent = {
    Auth,
    Group,
    Lesson,
    GroupLesson,
    Schedule,
    TeacherGroupLesson,
    TeacherLesson,
    User,
    Tenant,
    TenantSettings
};