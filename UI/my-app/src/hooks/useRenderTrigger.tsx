import { useState } from "react";

export const useRenderTrigger = () => {
    const [trigger, setTrigger] = useState(true);

    const rerender = () => {
        setTrigger((prev) => !prev);
    }

    return rerender;
}