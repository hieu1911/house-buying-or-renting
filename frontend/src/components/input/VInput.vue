<template>
    <div :class="{'input--w-100': w100}">
        <label
            v-if="label" 
            class="input__title" 
            :class="{'input--required': required}"
            @click="$refs.inputRef?.focus()"
        >{{ label }}</label>
        <div class="input__body" :class="{'input--w-100': w100}">
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
                @focus="handleFocus"
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
        <div class="error-wrapper">
            <p v-if="errorDesc && showErrorDesc" class="input__desc-error" :class="{'input__desc-error-limit': !w100}">{{ errorDesc }}</p>
        </div>
    </div>
</template>

<script setup>
import { defineEmits, defineProps, defineExpose, ref, computed, watch } from 'vue';
import { formatNumber, deFormatNumber } from '@/js/common/helper';

const emit = defineEmits(['update:modelValue', 'clickIcon', 'clear', 'tab', 'focus', 'blur', 'change'])

const props = defineProps({
    'label': String,
    'placeholder': String,
    'hasValidate': Boolean,
    'errorDesc': String,
    'hasIcon': Boolean,
    'iconType': String,
    'iconAfter': Boolean,
    'required': Boolean,
    'w100': Boolean,
    'modelValue': [String, Number],
    'combobox': Boolean,
    'small': Boolean,
    'readonly': Boolean,
    'disable': Boolean,
    'blurNotHideErr': Boolean,
    'password': Boolean,
    'h40': Boolean,
    'iconFilter': Boolean,
    'placeholderItalic': Boolean,
    'typeNumber': Boolean,
    'dateTime': Boolean,
    'toggleValue': Boolean,
    'numberFloat': Boolean,
    'noFormat': Boolean,
    'width280': Boolean,
    'noMinWidth': Boolean,
    'validateDup': Boolean
});

defineExpose({
    showError
})

const value = ref(props.modelValue)
const showErrorDesc = ref(false)
const showDeleteIcon = ref(false)
const showPassword = ref(false)
const inputType = ref("text")

if (props.password) {
    inputType.value = "password"
}

// function focus() {
//     this.$refs.inputRef?.focus();
// }

function handleBlur() {
    if (props.blurNotHideErr && props.modelValue) {
        return;
    }

    if (props.combobox) return;

    if (props.hasValidate && !props.modelValue) {
        showErrorDesc.value = true;
    } else {
        showErrorDesc.value = false;
    }

    emit('blur');
}

function handleFocus() {
    if (value.value) {
        showDeleteIcon.value = true;
    }
    emit('focus')
}

function showError() {
    showErrorDesc.value = true;
}

// function hideError() {
//     this.showErrorDesc = false;
// }

function deleteValue() {
    value.value = '';
    console.log(value.value);
    emit('update:modelValue', value.value);
    emit('clear');
    focus();
}

function handleClickIcon() {
    if (props.combobox) {
        focus();
    }
    emit('clickIcon', value.value);

    if (props.password) {
        showPassword.value = !showPassword.value;
    }
}

// function blur() {
//     this.$refs.inputRef.blur();
// }

const inputClass = computed(() => {
    return {
        'input--nomal': !showErrorDesc.value,
        'input--error': showErrorDesc.value,
        'input--have-icon-after': props.iconAfter,
        'input--w-100': props.w100,
        'input--small': props.small,
        'input--disable': props.disable,
        'input--hide-text': !props.showPassword && props.password,
        'input--height-40': props.h40,
        'input--placeholder-italic': props.placeholderItalic,
        'input--text-align-right': props.typeNumber || props.numberFloat,
        'input--w-280': props.width280,
        'input__content--no-min-width': props.noMinWidth,
        'input--readonly': props.readonly
    }
})

watch(value, (newValue, oldValue) => {
    if (newValue) {
        showDeleteIcon.value = true;
    } else {
        emit('clear');
        showDeleteIcon.value = false;
    }

    if (props.typeNumber && newValue) {
        const number = deFormatNumber(newValue.toString());

        if (isNaN(number)) {
            value.value = oldValue;
            return;
        }
        
        value.value = formatNumber(number);
        //console.log(value.value)
    }

    emit('update:modelValue', value.value);
    emit('change')
})

watch(() => props.modelValue, newValue => {
    value.value = newValue;
})
</script>

<style scoped>
@import url(./v-input.css);
</style>