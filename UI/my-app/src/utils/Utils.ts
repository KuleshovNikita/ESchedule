export const getEnumKey = (targetEnum: any, value: any) => {
    const key = Object.values(targetEnum).indexOf(value as unknown as any);
    return Object.keys(targetEnum)[key];
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

export const hourInPixels = 148;