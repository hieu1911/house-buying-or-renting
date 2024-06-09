<template>
    <div class="dialog-wrapper">
        <div class="dialog dialog-center">
            <div class="dialog__header">
                <h3 class="dialog__title">{{ dialogTitle }}</h3>
                <div class="dialog-close-btn">
                    <v-icon 
                        :desc="$t('icon.close')" 
                        descOnTop
                        type="close" 
                        @click="$emit('close')" 
                    ></v-icon>
                </div>
            </div>
            <div class="dialog__body">
                <v-icon :type="iconType"></v-icon>
                <div>
                    <li v-for="(item, index) in dialogDesc" :key="index" :class="{'list-style-none': dialogDesc.length < 2}">{{ item }}</li>
                </div>
            </div>
            <div class="dialog__footer">
                <v-button 
                    v-show="showCancelBtn" 
                    :label="$t('button.cancel')" 
                    type="secondary" 
                    ref="dialogCancelBtnRef"
                    @click="$emit('close')" 
                    @keydown.enter="$emit('close')"
                ></v-button>
                <v-button 
                    :label="showCancelBtn ? 'Xác nhận' : $t('button.accept')" 
                    type='primary' 
                    ref="dialogConfirmBtnRef"
                    @click="handleClickAccepBtn()" 
                    @keydown.enter="handleClickAccepBtn()"
                ></v-button>
            </div>
        </div>
    </div>
</template>

<script setup>
import { defineEmits, defineProps, computed, onMounted, inject, ref } from 'vue';

const emits = defineEmits(['close', 'action', 'moreAction']);
const props = defineProps({
    'dialogTitle': String,
    'dialogDesc': Array,
    'dialogType': Number,
    'showCancelBtn': Boolean,
    'del': Boolean,
});

const enums = inject('$enums');

const dialogConfirmBtnRef = ref(null)

const iconType = computed(() => {
    const statusEnum = enums.statusEnum;

    switch(props.dialogType) {
        case statusEnum.ERROR: return 'error';
        case statusEnum.WARNING: return 'warning';
        case statusEnum.SUCCESS: return 'success';
        case statusEnum.INFO: return 'info';
        default: return '';
    }
})

onMounted(() => {
    dialogConfirmBtnRef.value.focus();
});

function handleClickAccepBtn() {
    emits('close');
    emits('action');
}
</script>

<style>
@import url(./v-dialog.css);
</style>