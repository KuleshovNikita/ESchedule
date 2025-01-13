import { MutableRefObject, RefObject, useRef, useState } from "react";
import { useCult } from "../useTranslator";
import { EMAIL_REGEX, GUID_REGEX } from "../../utils/RegexConstants";

export type InputFocus = React.FocusEvent<HTMLInputElement | HTMLTextAreaElement, Element>; 
type InputHookType = 'guid' | 'email' | 'text' | 'number' | 'repeatPassword';

export type InputHookPayload = {
    ref: RefObject<HTMLInputElement>,
    value: string,
    errors: MutableRefObject<string>,
    handleChange: (e: InputFocus) => void,
    validate: (...args: any) => void
} 

export const useInput = (type: InputHookType, defaultValue: string = '', args: any[] = []) => {
    const { translator } = useCult();
    const ref = useRef<HTMLInputElement>(null);
    const [value, setValue] = useState(defaultValue);
    const errorsRef = useRef<string>('');

    let funcs: any;

    switch (type) {
        case 'guid': funcs = textHandler(translator, setValue, errorsRef, GUID_REGEX); break;
        case 'email': funcs = textHandler(translator, setValue, errorsRef, EMAIL_REGEX); break;
        case 'text': funcs = textHandler(translator, setValue, errorsRef); break;
        case 'repeatPassword': funcs = repeatPasswordHandler(translator, setValue, errorsRef, args[0]); break;
        case 'number': funcs = numberHandler(translator, setValue, errorsRef, args[0], args[1]); break;
    }

    const result: InputHookPayload = { 
        ref, 
        value, 
        errors: errorsRef, 
        handleChange: funcs.handleChange, 
        validate: funcs.validate 
    }

    return result;
}

const textHandler = (translator: (key: string) => string, setValue: any, errorsRef: MutableRefObject<string>, regex: RegExp | null = null) => {
    const handleChange = (e: InputFocus) => {
        const value = e.target.value;

        validate(value);

        setValue(value);
    }

    const validate = (value: string) => {
        if (value.length === 0) {
            errorsRef.current = translator('input-helpers.field-required');
        } else if (regex && !value.match(regex)) {
            errorsRef.current = translator('input-helpers.input-should-be-in-correct-format');
        } else {
            errorsRef.current = '';
        }
    }

    return { handleChange, validate };
}

const numberHandler = (translator: (key: string) => string, setValue: any, errorsRef: MutableRefObject<string>, min: number | null = null, max: number | null = min) => {
    const handleChange = (e: InputFocus) => {
        const num = Number(e.target.value);

        validate(num, min, max);

        setValue(num);
    }

    const validate = (num: number, min: number | null, max: number | null) => {
        if(!num || min && num < min || max && num > max) {
            errorsRef.current = translator('input-helpers.age-should-be-between-5-99');
        } else {
            errorsRef.current = '';
        }
    }

    return { handleChange, validate };
}

const repeatPasswordHandler = (translator: (key: string) => string, setValue: any, errorsRef: MutableRefObject<string>, password: string) => {
    const handleChange = (e: InputFocus) => {
        const value = e.target.value;

        validate(value, password);

        setValue(value);
    }    

    const validate = (value: string, password: string) => {
        if (value.length === 0) {
            errorsRef.current = translator('input-helpers.please-repeat-password');
        } else if (value !== password) {
            errorsRef.current = translator('input-helpers.passwords-do-not-match');
        } else {
            errorsRef.current = '';
        }
    }

    return { handleChange, validate };
}