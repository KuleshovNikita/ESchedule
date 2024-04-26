import { makeAutoObservable } from "mobx";
import { agent } from "../agent";
import { UserModel, UserLoginModel, UserCreateModel, UserUpdateModel, Role } from "../../models/Users";
import { store } from "./StoresManager";
import BaseStore from "./BaseStore";
import { toast } from "react-toastify";

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

        if (this.base.handleErrors(response)) {
            console.debug("login successful, token - " + response.value);
            store.commonStore.setToken(response.value);

            const userInfo = await this.getAutenticatedUserInfo();

            this.user = userInfo;
        }
        
        return response.isSuccessful;
    };

    logout = () => {
        store.commonStore.setToken(null);
        window.localStorage.removeItem("jwt");
        this.user = null;
    };

    updateUserInfo = async (user: UserUpdateModel) =>
        await this.base.simpleRequest(async () => await agent.User.updateUser(user));

    confirmEmail = async (key: string) => {
        const response = await agent.Auth.confirmEmail(key);

        this.base.handleErrors(response);

        return response;
    }

    register = async (creds: UserCreateModel) => {
        const response = await agent.Auth.register(creds);

        return this.base.handleErrors(response);
    }

    getAutenticatedUserInfo = async () => {    
        if(this.isLoggedIn) {
            return this.user;
        } else {
            const response = await agent.Auth.getAuthenticatedUserInfo();

            this.base.handleErrors(response);
            
            if(response.value !== null) {
                this.user = response.value;
            }

            return response.value;
        }
    }

    getUserInfo = async (userId: string) => {
        const response = await agent.User.getUser(userId);

        this.base.handleErrors(response);

        if(response.isSuccessful && !this.otherUsers.includes(response.value)) {
            this.otherUsers.push(response.value);
        }

        return response.value;
    }
}