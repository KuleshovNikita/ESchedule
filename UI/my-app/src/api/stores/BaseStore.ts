import { toast } from "react-toastify"
import { EmptyResult } from "../../models/Result"

export default abstract class BaseStore {
    abstract client: any;

    handleErrors = (response: EmptyResult) => {
        if(!response.isSuccessful) {
            toast.error(response.clientErrorMessage);
        }
    }

    simpleRequest = async (request: BaseRequest) => {
        const response = await request();
        this.handleErrors(response);
    }
}

export interface BaseRequest {
    (): Promise<EmptyResult>;
}