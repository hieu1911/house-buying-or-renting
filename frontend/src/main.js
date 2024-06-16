import { createApp } from 'vue'
import { createPinia } from 'pinia';
import VueTippy from 'vue-tippy';
import 'tippy.js/dist/tippy.css'; 
import emitter from 'tiny-emitter/instance';
import { createI18n } from 'vue-i18n';
import { VueFire, VueFireAuth } from 'vuefire';
import { firebaseApp } from './js/firebase';
import { VueSignalR } from '@dreamonkey/vue-signalr';
import { HubConnectionBuilder } from '@microsoft/signalr';

import router from './js/router/router'
import App from './App.vue'
import VInput from './components/input/VInput.vue';
import VButton from './components/button/VButton.vue';
import VIcon from './components/icon/VIcon.vue';
import VDialog from './components/dialog/VDialog.vue';
import VCombobox from './components/combobox/VCombobox.vue';
import VCheckbox from './components/checkbox/VCheckbox.vue';
import VToastMessage from './components/toast-message/VToastMessage.vue';
import messages from './js/resource';
import enums from './js/common/enum';
import common from './js/common/helper';

const app = createApp(App);
const pinia = createPinia();

const i18n = createI18n({
    locale: 'vi',
    fallbackLocale: 'en',
    messages
})

const connection = new HubConnectionBuilder()
  .withUrl('https://localhost:7198/chatHub')
  .withAutomaticReconnect()
  .build();

app.provide('$emitter', emitter)
app.provide('$router', router)
app.provide('$enums', enums)
app.provide('$common', common)

app
.component('v-input', VInput)
.component('v-button', VButton)
.component('v-icon', VIcon)
.component('v-dialog', VDialog)
.component('v-combobox', VCombobox)
.component('v-checkbox', VCheckbox)
.component('v-toast-message', VToastMessage)

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

app.use(VueFire, {
    firebaseApp,
    modules: [
      VueFireAuth(),
    ],
})

app.use(router);
app.use(pinia);
app.use(i18n);
app.use(VueSignalR, { connection });

app.mount('#app')
