import { createContext, useContext } from "react";
import CommonStore from "./CommonStore";
import GroupLessonStore from "./GroupLesson";
import GroupStore from "./GroupStore";
import LessonStore from "./LessonStore";
import ScheduleStore from "./ScheduleStore";
import TeacherGroupLessonStore from "./TeacherGroupLessonStore";
import TeacherLessonStore from "./TeacherLessonStore";
import TenantStore from "./Tenant";
import TenantSettingsStore from "./TenantSettings";
import UserStore from "./UserStore";

export interface CacheDisposable {
    clearCache: () => void
}

export interface Store {
    commonStore: CommonStore;
    userStore: UserStore;
    groupStore: GroupStore;
    lessonStore: LessonStore;
    groupLessonStore: GroupLessonStore;
    scheduleStore: ScheduleStore;
    teacherGroupLessonStore: TeacherGroupLessonStore;
    teacherLessonStore: TeacherLessonStore;
    tenantStore: TenantStore;
    tenantSettingsStore: TenantSettingsStore; 
}

export const store: Store = {
    commonStore: new CommonStore(),
    userStore: new UserStore(),
    groupStore: new GroupStore(),
    lessonStore: new LessonStore(),
    groupLessonStore: new GroupLessonStore(),
    scheduleStore: new ScheduleStore(),
    teacherGroupLessonStore: new TeacherGroupLessonStore(),
    teacherLessonStore: new TeacherLessonStore(),
    tenantStore: new TenantStore(),
    tenantSettingsStore: new TenantSettingsStore(),
}

export const StoreContext = createContext(store);

export function useStore() {
    return useContext(StoreContext);
}

export function clearStores(store: Store) {
    store.commonStore.clearCache();
    store.userStore.clearCache();
    store.groupStore.clearCache();
    store.lessonStore.clearCache();
    store.groupLessonStore.clearCache();
    store.scheduleStore.clearCache();
    store.teacherGroupLessonStore.clearCache();
    store.teacherLessonStore.clearCache();
    store.tenantStore.clearCache();
    store.tenantSettingsStore.clearCache();
}
