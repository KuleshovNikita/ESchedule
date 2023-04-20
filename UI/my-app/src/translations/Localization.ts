import i18n from 'i18next';

export const locales = {
    'en': 'English',
    'ua': 'Ukraine',
}

export const updateLang = (key: string) => {
    i18n.changeLanguage(key);
    localStorage.setItem('lang', key);
}

export const getDefLang = () => {
    let lang = localStorage.getItem('lang');

    if(!lang) {
        lang = 'en';
    } 

    i18n.changeLanguage(lang);
    return lang;
}