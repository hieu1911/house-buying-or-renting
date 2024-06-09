<template>
    <div :class="{'content': !store.isAuthPage}">
        <router-view></router-view>
        <div class="toast-message-wrapper" :style="topToastMessages">
            <transition-group name="toast">
                <div v-for="(toastMessage, index) in toastMessages" :key="toastMessage.content">
                    <v-toast-message 
                        :type="toastMessage.type"
                        :title="toastMessage.title"
                        :content="toastMessage.content"
                        @close="closeToastMessage(index)"
                    ></v-toast-message>
                </div>
            </transition-group>   
        </div>
        <v-dialog 
            v-if="dialogShowed"
            :dialogTitle="dialogTitle"
            :dialogDesc="dialogDesc"
            :dialogType="dialogType"
            :showCancelBtn="dialogShowCancelBtn"
            @close="closeDialog"
            @action="dialogAction"
        ></v-dialog>
        <div v-if="loadingShowed" class="loader-wrapper">
            <span class="loader"></span>
        </div>
        <div v-if="showFilter" class="filter-wrapper">
            <FilterCard @close="showFilter = false"></FilterCard>
        </div>
    </div>
</template>

<script setup>
import { onMounted, onUnmounted, inject, reactive } from 'vue';
import { ref } from 'vue';
import { publicStore } from '@/js/store/publicStore';

import FilterCard from '@/view/components/FilterCard/FilterCard.vue'

const loadingShowed = ref(false)
const showFilter = ref(false);

const dialogShowed = ref(false);
const dialogTitle = ref('');
const dialogDesc = ref('');
const dialogType = ref('');
const dialogShowCancelBtn = ref(false);
const toastMessages = reactive([]);
const topToastMessages = ref('top: 20px');
let dialogAction = null

const emitter = inject('$emitter');
const store = publicStore();

onMounted(() => {
    emitter.on('showDialog', showDialog);
    emitter.on('showLoading', showLoading);
    emitter.on('showToastMessage', showToastMessage);
    emitter.on('showFilter', () => showFilter.value = true);
    window.addEventListener("scroll", onScroll)
})

onUnmounted(() => {
    emitter.off('showDialog', showDialog);
    emitter.off('showLoading', showLoading);
    emitter.off('showToastMessage', showToastMessage);
    emitter.off('showFilter', showFilter.value = true);
    window.removeEventListener("scroll", onScroll)
})

function showDialog(type, title, desc, action=null) {
    dialogAction = null;
    dialogType.value = type;
    dialogTitle.value = title;
    dialogDesc.value = desc;
    dialogShowed.value = true;

    if (action != null) {
        dialogShowCancelBtn.value = true;
        dialogAction = action;
    } else {
        dialogShowCancelBtn.value = false;
        dialogAction = null;
    }
}

function closeDialog() {
    dialogShowed.value = false;
}

function showLoading(showed) {
    loadingShowed.value = showed;
}

function showToastMessage(type, title, content) {
    toastMessages.push({
        type,
        title,
        content: content
    });

    setTimeout(() => {
        if (toastMessages.length > 0) {
            toastMessages.shift();
        }
    }, 3000)
}

function closeToastMessage(index) {
    toastMessages.splice(index, 1);
}

function onScroll() {
    topToastMessages.value = `top: ${Math.floor(window.top.scrollY) + 20}px`
}

</script>

<style scoped>
@import url(./the-content.css);
</style>