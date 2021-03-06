﻿import '@fortawesome/fontawesome-free/css/all.css';
import '../../styles/theme/theme.sass';

import {docReady, onEvent} from "./utils";

docReady(() => {
    console.log('document ready');
});

// open the nav menu on clicking the navbar-burger class (on mobile)
onEvent('click', '.navbar-burger', (target: HTMLElement) => {
    let navTarget = document.getElementById(target.dataset.target as any);
    target.classList.toggle('is-active');
    navTarget?.classList.toggle('is-active');
});

// add 'is-loading' class to all element with 'type="submit"' on submitting a form
onEvent('submit', 'form', (target: HTMLElement) => {
    let submitButtons = target.querySelectorAll('[type="submit"]');
    submitButtons.forEach(value => {
        value.classList.add('is-loading');
        value.setAttribute('disabled', 'disabled');
    });

});
