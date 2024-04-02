<template>
    <div 
        :class="{'icon-option-more': moreOption, 'icon-disable': disable, 'icon-wrapper-small': smallWrapper, 'icon-wrapper': !nowrap, 'icon-wrapper-align-end': alignEnd}" 
        @click="$emit('click')"
        v-tippy="desc"
    >
        <div :class="{'icon-rotate': rotate, 'icon-transition-wrapper': transition}">
            <div class="icon" :class="'icon-'+type"></div>
        </div>
    </div>
</template>

<script setup>
defineProps({
    'type': String,
    'desc': String,
    'descOnTop': Boolean,
    'descPagination': Boolean,
    'moreOption': Boolean,
    'rotate': Boolean,
    'disable': Boolean,
    'transition': Boolean,
    'descMore': Boolean,
    'smallWrapper': Boolean,
    'nowrap': Boolean,
    'alignEnd': Boolean
})
defineEmits(['click'])
</script>

<style scoped>
@import url(./misa-icon.css);
</style>