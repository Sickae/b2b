export function docReady(fn: () => void) {
    if (document.readyState === "complete" || document.readyState === "interactive") {
        setTimeout(fn, 1);
    } else {
        document.addEventListener("DOMContentLoaded", fn);
    }
}

export function onEvent(eventName: string, elementSelector: string, callback: ((target: HTMLElement) => void) | ((target: HTMLElement, event: Event) => void)) {
    document.addEventListener(eventName as any, function (e) {
        // loop parent nodes from the target to the delegation node
        for (let target = e.target; target && target != this; target = target.parentNode) {
            if (target.matches(elementSelector)) {
                callback(target, e);
                break;
            }
        }
    }, false);
}

export default {docReady, addEvent: onEvent};
