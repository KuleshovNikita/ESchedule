import { useRef, useState } from "react";
import { useCult } from "./useTranslator";

const GUID_REGEX = /^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$/i;
const EMAIL_REGEX = /^[\w-.]+@([\w-]+\.)+[\w-]{2,4}$/

type Focus = React.FocusEvent<HTMLInputElement | HTMLTextAreaElement, Element>; 
type InputHookType = 'guid' | 'email';

export const useInput = (type: InputHookType) => {
    const { translator } = useCult();
    const ref = useRef<HTMLInputElement>(null);
    const [value, setValue] = useState('');
    const [errors, setErrors] = useState('');

    let handleChange: any;

    switch (type) {
        case 'guid': handleChange = guidHandler(translator, setValue, setErrors); break;
        case 'email': handleChange = emailHandler(translator, setValue, setErrors); break;
    }

    return { ref, value, errors, handleChange }
}

const guidHandler = (translator: (key: string) => string, setValue: any, setErrors: any) => {
    const handleChange = (e: Focus) => {
        const value = e.target.value;

        if (value.length === 0) {
            setErrors(translator('input-helpers.field-required'));
        } else if (!value.match(GUID_REGEX)) {
            setErrors(translator('input-helpers.field-wrong-format'));
        } else {
            setErrors('');
        }

        setValue(value);
    }

    return handleChange;
}

const emailHandler = (translator: (key: string) => string, setValue: any, setErrors: any) => {
    const handleChange = (e: Focus) => {
        const email = e.target.value;
    
        if(email.length === 0) {
            setErrors(translator('input-helpers.email-required'));
        } else if(!email.match(EMAIL_REGEX)) {
            setErrors(translator('input-helpers.email-should-be-correct'));
        } else {
            setErrors('');
        }
    
        setValue(email);
    }

    return handleChange;
}