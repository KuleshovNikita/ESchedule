import { makeAutoObservable, runInAction } from "mobx";
import { agent } from "../agent";
import { UserModel, UserLoginModel, UserCreateModel, UserUpdateModel } from "../../models/Users";
import { store } from "./StoresManager";
import BaseStore from "./BaseStore";


export default class UserStore extends BaseStore {
    client: any = null;
    user: UserModel | null = null;

    constructor() {
        super();
        makeAutoObservable(this);
    }

    get isLoggedIn() {
        return !!this.user;
    }

    login = async (creds: UserLoginModel) => {
        const response = await agent.Auth.login(creds);

        this.handleErrors(response);

        console.debug("login successful, token - " + response.value);
        store.commonStore.setToken(response.value);

        this.user = await this.getAutenticatedUserInfo();
        return response.isSuccessful;
    };

    logout = () => {
        store.commonStore.setToken(null);
        window.localStorage.removeItem("jwt");
        this.user = null;
    };

    updateUserInfo = async (user: UserUpdateModel) =>
        await this.simpleRequest(async () => await agent.User.updateUser(user));

    register = async (creds: UserCreateModel) => 
        await agent.Auth.register(creds);

    getAutenticatedUserInfo = async () => {
        if(this.isLoggedIn) {
            return this.user;
        } else {
            const response = await agent.Auth.getAuthenticatedUserInfo();

            this.handleErrors(response);

            return response.value;
        }
    }
}