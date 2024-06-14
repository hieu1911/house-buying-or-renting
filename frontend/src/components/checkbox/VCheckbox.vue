<template>
    <div class="checkbox-wrapper">
        <div>
            <input 
                :checked="modelValue"
                :disabled="disable"
                :indeterminate="indeterminate"
                ref="checkboxRef"
                type="checkbox" 
                @change="handleChange"
                @keydown.enter="click()"
            />
        </div>
        <label v-if="label" @click="$refs.checkboxRef.click()">{{ label }}</label>
    </div>
</template>

<script setup>
import { defineProps, defineEmits, ref } from 'vue';

defineProps({
    'modelValue': Boolean,
    'disable': Boolean,
    'label': String,
    'indeterminate': Boolean
})

const emit = defineEmits(['change', 'update:modelValue'])

const checkboxRef = ref(null);

function handleChange(event) {
    emit('update:modelValue', event.target.checked);
    emit('change', event.target.checked);
}

function click() {
    checkboxRef?.value.click();
}

// function focus() {
//     this.$refs.checkboxRef?.focus();
// }

// function activeCheckbox() {
//     if (!this.$refs.checkboxRef?.checked) {
//         this.click();
//     }
// }

</script>

<style>
@import url(./v-checkbox.css);
</style>