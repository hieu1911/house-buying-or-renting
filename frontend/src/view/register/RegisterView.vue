<template>
    <div class="register-wrapper">
        <div class="register-form">
            <h3 class="register-title">{{ $t('auth.register') }}</h3>
            <div class="register-field">
                <v-input 
                    :placeholder="$t('auth.fullName')"
                    v-model="fullName"
                    w100
                    h40
                ></v-input>
                <v-input 
                    :placeholder="$t('auth.phoneNumber')"
                    v-model="phoneNumber"
                    w100
                    h40
                ></v-input>
                <v-input 
                    :placeholder="$t('auth.email')"
                    v-model="email"
                    w100
                    h40
                ></v-input>
                <v-input 
                    :placeholder="$t('auth.password')"
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
import { ref } from 'vue'

const fullName = ref('')
const phoneNumber = ref('')
const email = ref('')
const password = ref('')
</script>

<style scoped>
@import url(./register-view.css);
</style>