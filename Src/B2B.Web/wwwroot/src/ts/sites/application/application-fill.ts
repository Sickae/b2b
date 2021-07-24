import {docReady, SiteConfig as cfg, onEvent} from "../utils";

let siteConfig: cfg.SiteConfigHandler;
const QUESTION_TYPE_KEYS: {[p: string]: string} = {
    Text: 'questionType-text',
    Choice: 'questionType-choice',
    MultiChoice: 'questionType-multi-choice'
};

docReady(() => {
    let containerId = 'site-config';
    siteConfig = cfg.load(containerId);
    for (let key in QUESTION_TYPE_KEYS)
        siteConfig.assertKeys([QUESTION_TYPE_KEYS[key]]);
});

onEvent<HTMLFormElement>('submit', 'form',(element, event) => {
    let formInput = document.querySelector<HTMLInputElement>('#FormJson');
    let formObj: {[p: string]: string | null} = {};
    let questions = document.querySelectorAll<HTMLElement>('.application-question')

    questions.forEach((x) => {
       let el = parseElement(x);
       if (el !== null)
           formObj[el.key] = el.value;

       // remove the 'name' attribute so it doesn't get submitted
        x.removeAttribute('name');
    });

    formInput!.value = JSON.stringify(formObj);
});

function parseElement(questionElement: HTMLElement): {key: string, value: string | null} | null {
    let codeInput = questionElement.querySelector<HTMLInputElement>('.question-code');
    if (codeInput === null)
        return null;

    let value: string | null = null;
    switch (questionElement.dataset.type) {
        case siteConfig.getValue(QUESTION_TYPE_KEYS.Text): {
            value = questionElement.querySelector<HTMLTextAreaElement>('textarea')?.value ?? null;
            break;
        }
        case siteConfig.getValue(QUESTION_TYPE_KEYS.Choice): {
            value = questionElement.querySelector<HTMLInputElement>('input[type="radio"]:checked')?.value ?? null
            break;
        }
        case siteConfig.getValue(QUESTION_TYPE_KEYS.MultiChoice): {
            let selected = questionElement.querySelectorAll<HTMLInputElement>('input[type="checkbox"]:checked');
            let labelArr = Array.from(selected).map(x => x.value ?? '');
            value = labelArr.join(';;');
            break;
        }
        default: break;
    }

    return value !== null && value.length > 0
        ? {key: codeInput.value, value}
        : null;
}
