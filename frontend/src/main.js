import { createApp } from 'vue'
import { createPinia } from 'pinia';
import VueTippy from 'vue-tippy';
import 'tippy.js/dist/tippy.css'; 
import emitter from 'tiny-emitter/instance';
import { createI18n } from 'vue-i18n';

import router from './js/router/router'
import App from './App.vue'
import VInput from './components/input/VInput.vue';
import VButton from './components/button/VButton.vue';
import VIcon from './components/icon/VIcon.vue';
import messages from './js/resource';

const app = createApp(App);
const pinia = createPinia();

const i18n = createI18n({
    locale: 'vi',
    fallbackLocale: 'en',
    messages
})

app.config.globalProperties.$router = router;
app.config.globalProperties.$emitter = emitter;

app
.component('v-input', VInput)
.component('v-button', VButton)
.component('v-icon', VIcon)

app.use(
    VueTippy,
    {
        directive: 'tippy',
        defaultProps: {
            theme: 'tomato',
            duration: [0, 0]
        }
    }
)

app.use(router);
app.use(pinia);
app.use(i18n);

app.mount('#app')
