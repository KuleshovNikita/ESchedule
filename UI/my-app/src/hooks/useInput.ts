import { useRef, useState } from "react";
import { useCult } from "./useTranslator";

const GUID_REGEX = /^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$/i;
const EMAIL_REGEX = /^[\w-.]+@([\w-]+\.)+[\w-]{2,4}$/

type Focus = React.FocusEvent<HTMLInputElement | HTMLTextAreaElement, Element>; 
type InputHookType = 'guid' | 'email' | 'text' | 'number';

export const useInput = (type: InputHookType, defaultValue: string = '', args: any[] = []) => {
    const { translator } = useCult();
    const ref = useRef<HTMLInputElement>(null);
    const [value, setValue] = useState(defaultValue);
    const [errors, setErrors] = useState('');

    let handleChange: any;

    switch (type) {
        case 'guid': handleChange = textHandler(translator, setValue, setErrors, GUID_REGEX); break;
        case 'email': handleChange = textHandler(translator, setValue, setErrors, EMAIL_REGEX); break;
        case 'text': handleChange = textHandler(translator, setValue, setErrors); break;
        case 'number': handleChange = numberHandler(translator, setValue, setErrors, args[0], args[1]); break;
    }

    return { ref, value, errors, handleChange }
}

const textHandler = (translator: (key: string) => string, setValue: any, setErrors: any, regex: RegExp | null = null) => {
    const handleChange = (e: Focus) => {
        const value = e.target.value;

        if (value.length === 0) {
            setErrors(translator('input-helpers.field-required'));
        } else if (regex && !value.match(regex)) {
            setErrors(translator('input-helpers.input-should-be-in-correct-format'));
        } else {
            setErrors('');
        }

        setValue(value);
    }

    return handleChange;
}

const numberHandler = (translator: (key: string) => string, setValue: any, setErrors: any, min: number | null = null, max: number | null = min) => {
    const handleChange = (e: Focus) => {
        const num = Number(e.target.value);

        if(!num || min && num < min || max && num > max) {
            setErrors(translator('input-helpers.age-should-be-between-5-99'));
        } else {
            setErrors('');
        }

        setValue(num);
    }

    return handleChange;
}