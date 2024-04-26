import { makeAutoObservable } from "mobx";
import { agent } from "../agent";
import { UserModel, UserLoginModel, UserCreateModel, UserUpdateModel, Role } from "../../models/Users";
import { store } from "./StoresManager";
import BaseStore from "./BaseStore";

export default class UserStore {
    base: BaseStore = new BaseStore();
    client: any = null;
    user: UserModel | null = null;
    otherUsers: UserModel[] = [];

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

        return response;
    };

    logout = () => {
        store.commonStore.setToken(null);
        window.localStorage.removeItem("jwt");
        this.user = null;
    };

    updateUserInfo = async (user: UserUpdateModel) => 
        await agent.User.updateUser(user);

    confirmEmail = async (key: string) => 
        await agent.Auth.confirmEmail(key);

    register = async (creds: UserCreateModel) => 
        await agent.Auth.register(creds);

    getAutenticatedUserInfo = async () => {    
        if(this.isLoggedIn) {
            return this.user;
        } else {
            const response = await agent.Auth.getAuthenticatedUserInfo();
            
            if(response) {
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
}