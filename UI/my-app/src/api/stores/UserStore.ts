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

        console.log("login successful, token - " + response.value);
        store.commonStore.setToken(response.value);
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
}