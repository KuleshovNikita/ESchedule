const loaderEvent = new CustomEvent('showLoader', {detail: {state: false}})

export const useLoader = () => {
    let state: boolean = false;

    const showLoader = () => {
        state = true;
        loaderEvent.detail.state = state;
        window.dispatchEvent(loaderEvent);
    }

    const hideLoader = () => {
        state = false;
        loaderEvent.detail.state = state;
        window.dispatchEvent(loaderEvent);
    }

    return {
        showLoader, 
        hideLoader,
        state
    };
}