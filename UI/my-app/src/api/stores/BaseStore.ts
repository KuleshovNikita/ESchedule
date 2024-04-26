import { toast } from "react-toastify";
import { EmptyResult } from "../../models/Result";
import i18n from "i18next";

export default class BaseStore {
    client: any;

    handleErrors = (response: EmptyResult): boolean => {
        if(!response.isSuccessful) {
            toast.error(i18n.t('server-errors.' + response.clientErrorMessage));
        }

        return response.isSuccessful;
    }

    simpleRequest = async (request: BaseRequest) => {
        const response = await request();
        this.handleErrors(response);

        return response;
    }
}

export interface BaseRequest {
    (): Promise<EmptyResult>;
}