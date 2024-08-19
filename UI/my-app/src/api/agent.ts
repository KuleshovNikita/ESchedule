import axios, { AxiosError, AxiosResponse } from "axios";
import { toast } from "react-toastify";
import { store } from "./stores/StoresManager";
import { redirect } from "react-router";
import { UserLoginModel, UserCreateModel, UserUpdateModel, UserModel, Role } from "../models/Users";
import { GroupCreateModel, GroupModel, GroupUpdateModel } from "../models/Groups";
import { LessonCreateModel, LessonModel, LessonUpdateModel } from "../models/Lessons";
import { GroupsLessonsCreateModel, GroupsLessonsModel, TeachersGroupsLessonsCreateModel, TeachersGroupsLessonsModel, TeachersLessonsCreateModel, TeachersLessonsModel } from "../models/ManyToMany";
import { RuleInputModel, RuleModel, ScheduleModel } from "../models/Schedules";
import { RequestTenantAccessModel, ScheduleStartEndTime, TenantCreateModel, TenantModel, TenantSettingsCreateModel, TenantSettingsModel, TenantSettingsUpdateModel, TenantUpdateModel } from "../models/Tenants";
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
                redirect('/login');
                break;
            case 403:
                toast.error(translator('You do not have access to this page'));
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
    updateUserTenant: (userId: string) => requests.patch(`/user/tenant/${userId}`),
    getUser: (id: string) => requests.get<UserModel>(`/user/${id}`),
    getUsersByRole: (role: Role) => requests.get<UserModel[]>(`/user/role/${role}`),
}

const Group = {
    createGroup: (body: GroupCreateModel) => requests.post(`/group`, body),
    updateGroup: (body: GroupUpdateModel) => requests.put(`/group`, body),
    getGroup: (id: string) => requests.get<GroupModel>(`/group/${id}`),
    getGroups: () => requests.get<GroupModel[]>(`/group`),
    removeGroup: (id: string) => requests.delete(`/group/${id}`),
}

const Lesson = {
    createLesson: (body: LessonCreateModel) => requests.post<LessonModel>(`/lesson`, body),
    removeLessons: (body: string[]) => requests.put("/lesson/many/", body),
    updateLesson: (body: LessonUpdateModel) => requests.put(`/lesson`, body),
    getLesson: (id: string) => requests.get<LessonModel>(`/lesson/${id}`),
    getLessons: () => requests.get<LessonModel[]>(`/lesson`),
    removeLesson: (id: string) => requests.delete(`/lesson/${id}`),
}

const Schedule = {
    buildSchedule: () => requests.post('/schedule', {}), 
    //TODO ---> updateSchedule: (rules: RuleInputModel[]) => requests.post(`/schedule/${tenantId}`, rules), 
    getScheduleForTenant: (tenantId: string) => requests.get<ScheduleModel[]>(`/schedule/tenant/${tenantId}`), 
    getScheduleRules: () => requests.get<RuleModel[]>('/schedule/rules/'), 
    getScheduleForGroup: (groupId: string) => requests.get<ScheduleModel[]>(`/schedule/group/${groupId}`), 
    getScheduleForTeacher: (teacherId: string) => requests.get<ScheduleModel[]>(`/schedule/teacher/${teacherId}`), 
    removeSchedule: () => requests.delete('/schedule'), 
    createRule: (rule: RuleInputModel) => requests.post(`/schedule/rule`, rule),
    removeRule: (ruleId: string) => requests.delete(`/schedule/rule/${ruleId}`),
    getScheduleItem: (id: string) => requests.get<ScheduleModel>(`/schedule/item/${id}`)
}

const Tenant = {
    createTenant: (body: TenantCreateModel) => requests.post<TenantModel>(`/tenant`, body),
    updateTenant: (body: TenantUpdateModel) => requests.put(`/tenant`, body),
    getTenant: (id: string) => requests.get<TenantModel>(`/tenant/${id}`),
    removeTenant: (id: string) => requests.delete(`/tenant/${id}`),
    sendTenantAccessRequest: (request: RequestTenantAccessModel) => requests.post('/tenant/request', request),
    getTenantAccessRequests: () => requests.get<UserModel[]>('/tenant/accessRequests'),
    acceptAccessRequest: (userId: string) => requests.delete(`/tenant/acceptAccessRequest/${userId}`),
    declineAccessRequest: (userId: string) => requests.delete(`/tenant/declineAccessRequest/${userId}`)
}

const TenantSettings = {
    createTenantSettings: (body: TenantSettingsCreateModel) => requests.post(`/tenantSettings`, body),
    updateTenantSettings: (body: TenantSettingsUpdateModel) => requests.put(`/tenantSettings`, body),
    getTenantSettings: (tenantId: string) => requests.get<TenantSettingsModel>(`/tenantSettings/${tenantId}`),
    getTenantScheduleTimes: () => requests.get<ScheduleStartEndTime[]>(`/tenantSettings/time`),
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

function translator(arg0: string): import("react-toastify").ToastContent<unknown> {
    throw new Error("Function not implemented.");
}
