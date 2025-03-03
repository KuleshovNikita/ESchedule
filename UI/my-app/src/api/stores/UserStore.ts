import { makeAutoObservable } from "mobx";
import { agent } from "../agent";
import { UserModel, UserLoginModel, UserCreateModel, UserUpdateModel, Role } from "../../models/Users";
import { CacheDisposable, clearStores, store } from "./StoresManager";

export default class UserStore implements CacheDisposable {
    client: any = null;
    user: UserModel | null = null;
    otherUsers: UserModel[] = [];
    teachers: UserModel[] = [];

    constructor() {
        makeAutoObservable(this);
    }

    get isLoggedIn() {
        return !!this.user;
    }

    login = async (creds: UserLoginModel) => {
        const response = await agent.Auth.login(creds);
        store.commonStore.setToken(response);

        const userInfo = await this.getAutenticatedUserInfo();

        this.user = userInfo;
        store.tenantStore.tenant = userInfo!.tenant;

        return response;
    };

    logout = () => {
        store.commonStore.setToken(null);
        window.localStorage.removeItem("jwt");
        clearStores(store);
    };

    updateUserInfo = async (user: UserUpdateModel) => {
        const updatedUser = await agent.User.updateUser(user);
        this.user = updatedUser;

        return updatedUser;
    }

    updateUserTenant = async (userId: string) => 
        await agent.User.updateUserTenant(userId);

    confirmEmail = async (key: string) => 
        await agent.Auth.confirmEmail(key);

    register = async (creds: UserCreateModel) => 
        await agent.Auth.register(creds);

    getAutenticatedUserInfo = async () => {    
        if(this.isLoggedIn) {
            return this.user;
        } else {
            const response = await agent.Auth.getAuthenticatedUserInfo()
                .catch(_ => null); //разобраться с этим завтра
            
            if(response) {
                store.tenantStore.tenant = response.tenant
                this.user = response;
            }

            return response;
        }
    }

    getUserInfo = async (userId: string) => {
        const response = await agent.User.getUser(userId);

        if(response && !this.otherUsers.includes(response)) {
            this.otherUsers.push(response);
        }

        return response;
    }

    getTeachers = async () => {
        if(this.teachers) {
            return this.teachers;
        }

        var res = await agent.User.getUsersByRole(Role.Teacher);

        this.teachers = res;

        return res;
    }
        
    clearCache = () => {
        this.user = null;
        this.otherUsers = [];
        this.teachers = [];
    }
}