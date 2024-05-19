<template>
    <button 
        class="btn" 
        ref="btnRef"
        :class="buttonClass" 
        :disabled="disable"
        @click="$emit('click')"
        v-tippy="description"
    >
        <v-icon v-if="icon" :type="icon" :nowrap="icon=='add'"></v-icon>
        <label for="btn">{{ label }}</label>
    </button>
</template>

<script setup>
import { defineProps, defineEmits, defineExpose } from 'vue';

const props = defineProps({
    'label': String,
    'type': String,
    'icon': String,
    'description': String,
    'disable': Boolean,
    'w100': Boolean,
    'h40': Boolean
})

defineEmits(['click']);
defineExpose({
    focus
});

function focus() {
    this.$refs.btnRef.focus();
}

function checkClassByType(allowTypes) {
    return allowTypes.includes(props.type);
}

const buttonClass = {
    'btn--primary': checkClassByType(['primary', 'hasIconPrimary', 'login']),
    'btn--secondary': checkClassByType(['secondary', 'filter', 'hasIconSecondary']),
    'btn--btn-with-icon': checkClassByType(['hasIcon', 'hasIconPrimary', 'filter', 'hasIconSecondary']),
    'btn--third': checkClassByType(['third']),
    'btn--link': checkClassByType(['link', 'loginLink']),
    'btn--login-link': checkClassByType(['loginLink']),
    'btn--delete': checkClassByType(['delete']),
    'btn-filter': checkClassByType(['filter']),
    'btn--delete-primary': checkClassByType(['deletePrimary']),
    'btn--w-100': props.w100,
    'btn--login': checkClassByType(['login']),
    'btn--height-40': props.h40
}
</script>

<style scoped>
@import url(./v-button.css);
</style>