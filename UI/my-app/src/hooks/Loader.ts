import { loaderEventName } from "../utils/Utils";

const loaderEvent = new CustomEvent(loaderEventName, {detail: {state: false}})

export const useLoader = () => {

    const show = () => {
        loaderEvent.detail.state = true;
        window.dispatchEvent(loaderEvent);
    }

    const hide = () => {
        loaderEvent.detail.state = false;
        window.dispatchEvent(loaderEvent);
    }

    return {
        show,
        hide
    };
}