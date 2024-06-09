<template>
    <div class="toast-message">
        <div class="toast-message__pseudo"></div>
        <div class="toast-message__body">
            <div class="toast-message__content">
                <v-icon :type="iconType"></v-icon>
                <div>
                    <span class="toast-message__title" :class="typeClass">{{ title }}</span>
                    <span class="toast-message__desc">{{ content }}</span>
                </div>
            </div>
            <div @click="$emit('close')">
                <v-icon type="close"></v-icon>
            </div>
        </div>
    </div>
</template>

<script setup>
import { defineProps, computed } from 'vue';

const props = defineProps({
    'type': String,
    'content': String,
    'title': String
});

const iconType = computed(() => {
    if (props.type == 'success') return 'success-tm';
    if (props.type == 'warning') return 'warning-tm';
    if (props.type == 'error') return 'error-tm';
    if (props.type == 'info') return 'info-tm';

    return '';
});
        
const typeClass = computed(() => {
    return {
        'toast-message__type--success': props.type == 'success',
        'toast-message__type--warning': props.type == 'warning',
        'toast-message__type--error': props.type == 'error',
        'toast-message__type--info': props.type == 'info'
    }
});

</script>

<style scoped>
@import url(./v-toast-message.css);
</style>