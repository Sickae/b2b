export function onEvent<T extends HTMLElement>(eventName: string, elementSelector: string, callback: ((target: T) => void) | ((target: T, event: Event) => void)) {
    document.addEventListener(eventName as any, function (e) {
        // loop parent nodes from the target to the delegation node
        for (let target = e.target; target && target != this; target = target.parentNode) {
            if (target.matches(elementSelector)) {
                callback(target as T, e);
                break;
            }
        }
    }, false);
}

export default {onEvent};
