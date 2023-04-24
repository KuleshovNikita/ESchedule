import { TeacherBusyDayRule } from "../models/Rules";
import { RuleModel } from "../models/Schedules";

export const getEnumKey = (targetEnum: any, value: any) => {
    const key = Object.values(targetEnum).indexOf(value as unknown as any);
    return Object.keys(targetEnum)[key];
}

export const timeTableScope = { start: 8, end: 18 };

export const pascalToDashCase = (str: string) => 
    str.replace(/[A-Z]/g, letter => `-${letter.toLowerCase()}`).substring(1);

export const daysOfWeek = [ 
    'Sunday',
    'Monday', 
    'Tuesday', 
    'Wednesday', 
    'Thursday', 
    'Friday', 
    'Saturday'
]

export const hourInPixels = 148;