export const pascalToDashCase = (str: string) => 
    str.replace(/[A-Z]/g, letter => `-${letter.toLowerCase()}`).substring(1);

export const availableRules = [
    'rule.teacher-busy-day'
]