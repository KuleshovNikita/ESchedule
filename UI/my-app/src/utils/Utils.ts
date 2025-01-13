import { UserModel } from "../models/Users";
import { GUID_REGEX } from "./RegexConstants";

export const getEnumKey = (targetEnum: any, value: any) => {
    const key = Object.values(targetEnum).indexOf(value as unknown as any);
    return Object.keys(targetEnum)[key];
}

export const normalizeUserName = (user: UserModel | null) => {
    if(user) {
        return `${user.lastName} ${user.lastName[0]}. ${user.fatherName[0]}.`;
    } else {
        return "";
    }
}

export const isGuid = (value: string) => {
    return GUID_REGEX.test(value);
}

export const timeTableScope = { start: 8, end: 18 };

export const daysOfWeek = [ 
    'Sunday',
    'Monday', 
    'Tuesday', 
    'Wednesday', 
    'Thursday', 
    'Friday', 
    'Saturday'
]

export const noneWord = 'words.none';
export const loaderEventName = 'showLoader';


export const hourInPixels = 148;