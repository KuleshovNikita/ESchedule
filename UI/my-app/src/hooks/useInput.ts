import { useRef, useState } from "react";
import { useCult } from "./useTranslator";

var GUID_REGEX = /^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$/i;
type Focus = React.FocusEvent<HTMLInputElement | HTMLTextAreaElement, Element>; 
type InputHookType = 'guid';

export const useInput = (type: InputHookType) => {
    const { translator } = useCult();
    const ref = useRef(null);
    const [value, setValue] = useState('');
    const [errors, setErrors] = useState('');

    let handleChange: any;

    switch (type) {
        case 'guid': handleChange = guidHandler(translator, setValue, setErrors)
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