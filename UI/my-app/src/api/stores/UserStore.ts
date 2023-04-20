import { makeAutoObservable } from "mobx";
import { agent } from "../agent";
import { UserModel, UserLoginModel, UserCreateModel, UserUpdateModel, Role } from "../../models/Users";
import { store } from "./StoresManager";
import BaseStore from "./BaseStore";
import { toast } from "react-toastify";
import { t } from "i18next";

export default class UserStore {
    base: BaseStore = new BaseStore();
    client: any = null;
    user: UserModel | null = null;

    constructor() {
        makeAutoObservable(this);
    }

    get isLoggedIn() {
        return !!this.user;
    }

    login = async (creds: UserLoginModel) => {
        const response = await agent.Auth.login(creds);

        this.base.handleErrors(response);

        console.debug("login successful, token - " + response.value);
        store.commonStore.setToken(response.value);

        const userInfo = await this.getAutenticatedUserInfo();
        if(userInfo?.role === Role.Pupil) {
            this.logout();
            toast.error("Pupils are not allowed to access the web system");
            return false;
        } 

        this.user = userInfo;
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

        return response.isSuccessful;
    }

    register = async (creds: UserCreateModel) => 
        await agent.Auth.register(creds);

    getAutenticatedUserInfo = async () => {    
        if(this.isLoggedIn) {
            return this.user;
        } else {
            const response = await agent.Auth.getAuthenticatedUserInfo();

            toast.error(t('ERR0011'));

            this.base.handleErrors(response);
            
            if(response.value !== null) {
                this.user = response.value;
            }

            return response.value;
        }
    }
}