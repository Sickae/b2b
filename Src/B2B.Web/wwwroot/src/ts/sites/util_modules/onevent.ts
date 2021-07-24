export default function onEvent(eventName: string, elementSelector: string, callback: ((target: HTMLElement) => void) | ((target: HTMLElement, event: Event) => void)) {
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
