import { Store } from "./api/stores/StoresManager";

export default async function AppLoader(stores: Store) {
    await stores.userStore.getAutenticatedUserInfo();

    stores.commonStore.setAppLoaded(true);
}