<template>
    <div>
        <label 
            v-if="label" 
            class="input__title" 
            :class="{'input--required': required}"
            @click="$refs.inputRef?.focus()"
        >{{ label }}</label>
        <div class="input__body" :class="{'input--w-100': width100}">
            <input 
                :class="inputClass" 
                :placeholder="placeholder" 
                :readonly="readonly"
                :disabled="disable"
                :type="inputType"
                class="input__content" 
                ref="inputRef"
                v-model="value"
                maxlength="250"
                @blur="handleBlur"
                @keydown.enter="$emit('clickIcon', value)"
                @keydown.tab="$emit('tab')"
                @focus="$emit('focus')"
            />
            <div v-if="showDeleteIcon && !combobox && !password && !disable" class="input-delete-icon" @click="deleteValue">
                <v-icon type="close-small"></v-icon>
            </div>
            <div v-if="iconFilter" class="input__filter">
                <v-icon type="filter"></v-icon>
            </div>
            <div 
                v-if="hasIcon || password" 
                :class="{'input__icon--after': iconAfter || password, 'input__icon': !(iconAfter || password)}" 
                @click="handleClickIcon"
            >
                <v-icon v-if="password" :type="showPassword ? 'show-pass' : 'hide-pass'"></v-icon>
                <v-icon :type="iconType" :disable="disable" v-else></v-icon>
            </div>
        </div>
        <p v-if="errorDesc && showErrorDesc" class="input__desc-error" :class="{'input__desc-error-limit': !width100}">{{ errorDesc }}</p>
    </div>
</template>

<script setup>
import { formatNumber, deFormatNumber } from '@/js/helpers/format-data';

defineEmits(['update:modelValue', 'clickIcon', 'clear', 'tab', 'focus', 'blur', 'change'])

defineProps({
    'label': String,
    'placeholder': String,
    'hasValidate': Boolean,
    'errorDesc': String,
    'hasIcon': Boolean,
    'iconType': String,
    'iconAfter': Boolean,
    'required': Boolean,
    'width100': Boolean,
    'modelValue': [String, Number],
    'combobox': Boolean,
    'small': Boolean,
    'readonly': Boolean,
    'disable': Boolean,
    'blurNotHideErr': Boolean,
    'password': Boolean,
    'inputLoginForm': Boolean,
    'iconFilter': Boolean,
    'placeholderItalic': Boolean,
    'typeNumber': Boolean,
    'dateTime': Boolean,
    'toggleValue': Boolean,
    'numberFloat': Boolean,
    'noFormat': Boolean,
    'width280': Boolean,
    'noMinWidth': Boolean,
    'validateDup': Boolean,
    'max': Number
})

const value = ref(modelValue)
const showErrorDesc = ref(false)
const showDeleteIcon = ref(false)
const showPassword = ref(false)

function focus() {
    this.$refs.inputRef?.focus();
}

function handleBlur() {
    if (this.blurNotHideErr && this.modelValue) {
        return;
    }

    if (this.combobox) return;

    if (this.hasValidate && !this.modelValue) {
        this.showErrorDesc = true;
    } else {
        this.showErrorDesc = false;
    }

    this.$emit('blur');
}

function showError() {
    this.showErrorDesc = true;
}

function hideError() {
    this.showErrorDesc = false;
}

function deleteValue() {
    this.value = '';
    this.$emit('update:modelValue', this.value);
    this.$emit('clear');
    this.focus();
}

function andleClickIcon() {
    if (this.combobox) {
        this.focus();
    }
    this.$emit('clickIcon', this.value);

    if (this.password) {
        this.showPassword = !this.showPassword;
    }
}

function blur() {
    this.$refs.inputRef.blur();
}

const inputClass = computed(() => {
    return {
        'input--nomal': !this.showErrorDesc,
        'input--error': this.showErrorDesc,
        'input--have-icon-after': this.iconAfter,
        'input--w-100': this.width100,
        'input--small': this.small,
        'input--disable': this.disable,
        'input--hide-text': !this.showPassword && this.password,
        'input--height-large': this.inputLoginForm,
        'input--placeholder-italic': this.placeholderItalic,
        'input--text-align-right': this.typeNumber || this.numberFloat,
        'input--w-280': this.width280,
        'input__content--no-min-width': this.noMinWidth
    }
})

watch(value, (newValue, oldValue) => {
    if (newValue) {
        this.showDeleteIcon = true;
    } else {
        this.$emit('clear');
        this.showDeleteIcon = false;
    }

    if (newValue != oldValue && !this.validateDup) {
        this.showErrorDesc = false;
    }

    this.$emit('update:modelValue', this.value);
    this.$emit('change')
})

watch(modelValue, (newValue) => {
    this.value = newValue
})
</script>

<style scoped>
@import url(./v-input.css);
</style>