<template>
    <div class="register-wrapper">
        <div class="register-form">
            <h3 class="register-title">{{ $t('auth.register') }}</h3>
            <div class="register-field">
                <v-input 
                    :placeholder="$t('auth.fullName')"
                    :errorDesc="$t('auth.fullNameEmpty')"
                    ref="fullNameRef"
                    v-model="fullName"
                    w100
                    h40
                ></v-input>
                <v-input 
                    :placeholder="$t('auth.phoneNumber')"
                    :errorDesc="$t('auth.phoneNumberEmpty')"
                    ref="phoneNumberRef"
                    v-model="phoneNumber"
                    w100
                    h40
                ></v-input>
                <v-input 
                    :placeholder="$t('auth.email')"
                    :errorDesc="$t('auth.emailEmpty')"
                    ref="emailRef"
                    v-model="email"
                    w100
                    h40
                ></v-input>
                <v-input 
                    :placeholder="$t('auth.password')"
                    :errorDesc="$t('auth.passwordEmpyt')"
                    ref="passwordRef"
                    v-model="password"
                    password
                    w100
                    h40
                ></v-input>
            </div>
            <v-button
                :label="$t('auth.register')"
                type="primary"
                w100
                h40
                @click="handleRegister"
            ></v-button>
            <div class="other-way-register-title">
                <hr/>
                <span>{{ $t('auth.otherWayToLogin') }}</span>
                <hr/>
            </div>
            <div class="other-way-register">
                <div class="other-way-register-item">
                    <img src="../../assets/icon/facebook.png" />
                    <span>Facebook</span>
                </div>
                <div class="other-way-register-item">
                    <img src="../../assets/icon/google.png" />
                    <span>Google</span>
                </div>
                <div class="other-way-register-item">
                    <img src="../../assets/icon/apple-logo.png" />
                    <span>Apple</span>
                </div>
            </div>
            <div class="register-account">
                <span>{{ $t('auth.hasAccount') }}</span>
                <RouterLink to="login">{{ $t('auth.loginNow') }}</RouterLink>
            </div>
        </div>
    </div>
</template>

<script setup>
import { ref, inject } from 'vue'
import { register } from '@/js/service/auth';
import full from 'core-js/full';

const router = inject('$router');

const fullNameRef = ref(null)
const phoneNumberRef = ref(null)
const emailRef = ref(null)
const passwordRef = ref(null)

const fullName = ref('')
const phoneNumber = ref('')
const email = ref('')
const password = ref('')

async function handleRegister() {
    if (fullName.value.trim() == '') {
        fullNameRef.value.showError();
        return;
    }

    if (phoneNumber.value.trim() == '') {
        phoneNumberRef.value.showError();
        return;
    }

    if (email.value.trim() == '') {
        emailRef.value.showError();
        return;
    }

    if (password.value.trim() == '') {
        passwordRef.value.showError();
        return;
    }

    const response = await register({
        Fullname: fullName.value,
        PhoneNumber: phoneNumber.value,
        Email: email.value,
        Password: password.value
    });

    if (response) {
        router.push('/login');
    } else {
        console.log(response)
    }
}
</script>

<style scoped>
@import url(./register-view.css);
</style>