import { InputHookPayload } from "./useInput";

export const useInputValidator = () => {
    const hasErrors = (...args: InputHookPayload[]) => {
        validateEachInput(...args);

        for(let i = 0; i < args.length; i++) {
            if (args[i].errors.current !== '') {
                return true;
            }
        }

        return false;
    }

    const validateEachInput = (...args: InputHookPayload[]) => 
        args.forEach(el => {
            el.ref.current?.focus();
        }
    );

    return hasErrors;
}