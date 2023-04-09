import { Store } from "./api/stores/StoresManager";

export default async function AppLoader(stores: Store) {
    const response = await stores.userStore.getAutenticatedUserInfo();

    stores.commonStore.setAppLoaded(true);
}