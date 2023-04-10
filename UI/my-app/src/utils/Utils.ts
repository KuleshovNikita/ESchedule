export const getEnumKey = (targetEnum: any, value: any) => {
    const key = Object.values(targetEnum).indexOf(value as unknown as any);
    return Object.keys(targetEnum)[key];
}