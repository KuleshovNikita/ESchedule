import axios, { AxiosError, AxiosResponse } from "axios";
import { toast } from "react-toastify";
import { store } from "./stores/StoresManager";
import { redirect } from "react-router";
import { UserLoginModel, UserCreateModel, UserUpdateModel, UserModel } from "../models/Users";
import { GroupCreateModel, GroupModel, GroupUpdateModel } from "../models/Groups";
import { LessonCreateModel, LessonModel, LessonUpdateModel } from "../models/Lessons";
import { EmptyResult, Result } from "../models/Result";
import { GroupsLessonsCreateModel, GroupsLessonsModel, TeachersGroupsLessonsCreateModel, TeachersGroupsLessonsModel, TeachersLessonsCreateModel, TeachersLessonsModel } from "../models/ManyToMany";
import { RuleInputModel, RuleModel, ScheduleModel } from "../models/Schedules";
import { ScheduleStartEndTime, TenantCreateModel, TenantModel, TenantSettingsCreateModel, TenantSettingsModel, TenantSettingsUpdateModel, TenantUpdateModel } from "../models/Tenants";

interface ErrorResponse {
    errors: { detail: string }[];
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
                if (data.errors) {
                    console.log(data);
                    const modalStateErrors = [];

                    for (const key in data.errors) {
                        if (data.errors[key]) {
                            toast.error((data.errors[key] as unknown as Array<string>)[0]);
                            modalStateErrors.push(data.errors[key]);
                        }
                    }
                    throw modalStateErrors.flat();
                } else {
                    toast.error(data.errors);
                }
                break;
            case 401:
                console.log(401)
                if (headers['www-authenticate']?.startsWith('Bearer error="invalid_token"')) {
                    store.userStore.logout();
                    toast.error('Session has expired - please login again');
                }
                break;
            case 404:
                redirect("/notFound");
                break;
            case 500:
                data.errors.map(e => toast.error(e.detail));                
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
    login: (body: UserLoginModel) => requests.post<Result<string>>("/authentication/login", body),
    register: (body: UserCreateModel) => requests.post<Result<string>>("/authentication/register", body),
    getAuthenticatedUserInfo: () => requests.get<Result<UserModel>>("/authentication"),
    confirmEmail: (key: string) => requests.patch<EmptyResult>(`/authentication/confirmEmail/${key}`)
}

const User = {
    updateUser: (body: UserUpdateModel) => requests.put<EmptyResult>("/user", body),
    getUser: (id: string) => requests.get<Result<UserModel>>(`/user/${id}`),
    removeUser: (id: string) => requests.delete<EmptyResult>(`/user/${id}`),
}

const Group = {
    createGroup: (body: GroupCreateModel) => requests.post<EmptyResult>(`/group`, body),
    updateGroup: (body: GroupUpdateModel) => requests.put<EmptyResult>(`/group`, body),
    getGroup: (id: string) => requests.get<Result<GroupModel>>(`/group/${id}`),
    removeGroup: (id: string) => requests.delete<EmptyResult>(`/group/${id}`),
}

const Lesson = {
    createLesson: (body: LessonCreateModel) => requests.post<EmptyResult>(`/lesson`, body),
    updateLesson: (body: LessonUpdateModel) => requests.put<EmptyResult>(`/lesson`, body),
    getLesson: (id: string) => requests.get<Result<LessonModel>>(`/lesson/${id}`),
    removeLesson: (id: string) => requests.delete<EmptyResult>(`/lesson/${id}`),
}

const Schedule = {
    buildSchedule: (tenantId: string) => requests.post<EmptyResult>(`/schedule/${tenantId}`, {}), 
    //TODO ---> updateSchedule: (rules: RuleInputModel[]) => requests.post<EmptyResult>(`/schedule/${tenantId}`, rules), 
    getScheduleForTenant: (tenantId: string) => requests.get<Result<ScheduleModel[]>>(`/schedule/tenant/${tenantId}`), 
    getScheduleRules: (tenantId: string) => requests.get<Result<RuleModel[]>>(`/schedule/rules/${tenantId}`), 
    getScheduleForGroup: (groupId: string) => requests.get<Result<ScheduleModel[]>>(`/schedule/group/${groupId}`), 
    getScheduleForTeacher: (teacherId: string) => requests.get<Result<ScheduleModel[]>>(`/schedule/teacher/${teacherId}`), 
    removeSchedule: (tenantId: string) => requests.delete<EmptyResult>(`/schedule/${tenantId}`), 
    createRule: (rule: RuleInputModel) => requests.post<EmptyResult>(`/schedule/rule`, rule),
    getScheduleItem: (id: string) => requests.get<Result<ScheduleModel>>(`/schedule/item/${id}`)
}

const Tenant = {
    createTenant: (body: TenantCreateModel) => requests.post<EmptyResult>(`/tenant`, body),
    updateTenant: (body: TenantUpdateModel) => requests.put<EmptyResult>(`/tenant`, body),
    getTenant: (id: string) => requests.get<Result<TenantModel>>(`/tenant/${id}`),
    removeTenant: (id: string) => requests.delete<EmptyResult>(`/tenant/${id}`),
    getTeachers: (id: string) => requests.get<Result<UserModel[]>>(`/tenant/teachers/${id}`),
    getGroups: (tenantId: string) => requests.get<Result<GroupModel[]>>(`/tenant/groups/${tenantId}`),
}

const TenantSettings = {
    createTenantSettings: (body: TenantSettingsCreateModel) => requests.post<EmptyResult>(`/tenantSettings`, body),
    updateTenantSettings: (body: TenantSettingsUpdateModel) => requests.put<EmptyResult>(`/tenantSettings`, body),
    getTenantSettings: (tenantId: string) => requests.get<Result<TenantSettingsModel>>(`/tenantSettings/${tenantId}`),
    getTenantScheduleTimes: (tenantId: string) => requests.get<Result<ScheduleStartEndTime[]>>(`/tenantSettings/time/${tenantId}`),
    removeTenantSettings: (tenantId: string) => requests.delete<EmptyResult>(`/tenantSettings/${tenantId}`),
}

const TeacherLesson = {
    createItems: (body: TeachersLessonsCreateModel) => requests.post<EmptyResult>(`/teachersLessons`, body),
    getItems: (id: string) => requests.get<Result<TeachersLessonsModel>>(`/teachersLessons/${id}`),
    removeItems: (id: string) => requests.delete<EmptyResult>(`/teachersLessons/${id}`),
}

const TeacherGroupLesson = {
    createItems: (body: TeachersGroupsLessonsCreateModel) => requests.post<EmptyResult>(`/teachersGroupsLessons`, body),
    getItems: (id: string) => requests.get<Result<TeachersGroupsLessonsModel>>(`/teachersGroupsLessons/${id}`),
    removeItems: (id: string) => requests.delete<EmptyResult>(`/teachersGroupsLessons/${id}`),
}

const GroupLesson = {
    createItems: (body: GroupsLessonsCreateModel) => requests.post<EmptyResult>(`/groupLessons`, body),
    getItems: (id: string) => requests.get<Result<GroupsLessonsModel>>(`/groupLessons/${id}`),
    removeItems: (id: string) => requests.delete<EmptyResult>(`/groupLessons/${id}`),
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