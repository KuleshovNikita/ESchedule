import { useRef, useState } from "react";
import { useCult } from "./useTranslator";

const GUID_REGEX = /^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$/i;
const EMAIL_REGEX = /^[\w-.]+@([\w-]+\.)+[\w-]{2,4}$/

type Focus = React.FocusEvent<HTMLInputElement | HTMLTextAreaElement, Element>; 
type InputHookType = 'guid' | 'email' | 'password';

export const useInput = (type: InputHookType) => {
    const { translator } = useCult();
    const ref = useRef<HTMLInputElement>(null);
    const [value, setValue] = useState('');
    const [errors, setErrors] = useState('');

    let handleChange: any;

    switch (type) {
        case 'guid': handleChange = textHandler(translator, setValue, setErrors, GUID_REGEX); break;
        case 'email': handleChange = textHandler(translator, setValue, setErrors, EMAIL_REGEX); break;
        case 'password': handleChange = textHandler(translator, setValue, setErrors); break;
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