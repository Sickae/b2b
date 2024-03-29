﻿import '../../../styles/sites/application.sass';
import SiteConfig from '../utils/siteconfig';
import {docReady} from "../utils/docready";
import {onEvent} from "../utils/onevent";

const QUESTION_TYPE_KEYS: {[p: string]: string} = {
    Text: 'questionType-text',
    Choice: 'questionType-choice',
    MultiChoice: 'questionType-multi-choice'
};

docReady(() => {
    for (let key in QUESTION_TYPE_KEYS)
        SiteConfig.assertKeys([QUESTION_TYPE_KEYS[key]]);

    reloadFromFormJson();
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
        case SiteConfig.getValue(QUESTION_TYPE_KEYS.Text): {
            value = questionElement.querySelector<HTMLTextAreaElement>('textarea')?.value ?? null;
            break;
        }
        case SiteConfig.getValue(QUESTION_TYPE_KEYS.Choice): {
            value = questionElement.querySelector<HTMLInputElement>('input[type="radio"]:checked')?.value ?? null
            break;
        }
        case SiteConfig.getValue(QUESTION_TYPE_KEYS.MultiChoice): {
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

function reloadFromFormJson(): void {
    let json = document.querySelector<HTMLInputElement>('#FormJson')?.value ?? '';
    if (json.length === 0)
        return;

    let form = JSON.parse(json);
    for (let key in form) {
        let input = document.querySelector<HTMLInputElement>(`[name="${key}"]`);
        let question = input?.closest<HTMLElement>('.application-question');

        if (input == null || question == null)
            continue;

        switch (question.dataset.type) {
            case SiteConfig.getValue(QUESTION_TYPE_KEYS.Text): {
                input.value = form[key];
                break;
            }
            case SiteConfig.getValue(QUESTION_TYPE_KEYS.Choice): {
                let radioInput = question.querySelector<HTMLInputElement>(`input[type="radio"][value="${form[key]}"]`);
                if (radioInput !== null)
                    radioInput.checked = true;
                break;
            }
            case SiteConfig.getValue(QUESTION_TYPE_KEYS.MultiChoice): {
                let values = form[key].split(';;');
                for (let value of values) {
                    let checkboxInput = question.querySelector<HTMLInputElement>(`input[type="checkbox"][value="${value}"]`);
                    if (checkboxInput !== null)
                        checkboxInput.checked = true;
                }
                break;
            }
            default: break;
        }
    }
}
