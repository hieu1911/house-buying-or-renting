<template>
    <div class="login-wrapper">
        <div class="login-form">
            <h3 class="login-title">{{ $t('auth.title') }}</h3>
            <div class="login-field">
                <v-input 
                    :placeholder="$t('auth.phoneNumberOrGmail')"
                    ref="phoneOrEmailRef"
                    v-model="phoneNumberOrGmail"
                    w100
                    h40
                ></v-input>
                <v-input 
                    :placeholder="$t('auth.password')"
                    ref="passwordRef"
                    v-model="password"
                    password
                    w100
                    h40
                ></v-input>
            </div>
            <p class="forgot-password">{{ $t('auth.forgotPassword') }}</p>
            <v-button
                :label="$t('auth.signIn')"
                type="primary"
                w100
                h40
                @click="handleLogin"
            ></v-button>
            <div class="other-way-login-title">
                <hr/>
                <span>{{ $t('auth.otherWayToLogin') }}</span>
                <hr/>
            </div>
            <div class="other-way-login">
                <div class="other-way-login-item">
                    <img src="../../assets/icon/facebook.png" />
                    <span>Facebook</span>
                </div>
                <div class="other-way-login-item">
                    <img src="../../assets/icon/google.png" />
                    <span>Google</span>
                </div>
                <div class="other-way-login-item">
                    <img src="../../assets/icon/apple-logo.png" />
                    <span>Apple</span>
                </div>
            </div>
            <div class="register-account">
                <span>{{ $t('auth.noAccount') }}</span>
                <RouterLink to="register">{{ $t('auth.registerNewAccount') }}</RouterLink>
            </div>
        </div>
    </div>
</template>

<script setup>
import { ref } from 'vue';
import { login } from '@/js/service/auth'

const phoneNumberOrGmail = ref('');
const password = ref('');

async function handleLogin() {
    if (phoneNumberOrGmail.value.trim() == '') {
        // phoneNumberOrGmail
        return;
    }

    if (password.value.trim() == '') {
        return;
    }

    const response = await login({
        PhoneOrEmail: phoneNumberOrGmail.value,
        Password: password.value
    })

    console.log(response);
    if (response) {
        $router.push('/')
    } else {

    }
}
</script>

<style scoped>
@import url(./login-view.css);
</style>