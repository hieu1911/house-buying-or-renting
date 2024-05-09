<template>
    <div :class="{'content': !store.isAuthPage}">
        <router-view></router-view>
        <v-dialog 
            v-if="dialogShowed"
            :dialogTitle="dialogTitle"
            :dialogDesc="dialogDesc"
            :dialogType="dialogType"
            :showCancelBtn="dialogShowCancelBtn"
            :del="dialogDelete"
            @close="closeDialog"
            @action="dialogAction"
        ></v-dialog>
        <div v-if="loadingShowed" class="loader-wrapper">
            <span class="loader"></span>
        </div>
    </div>
</template>

<script setup>
import { onMounted, onUnmounted, inject } from 'vue';
import { ref } from 'vue';
import { publicStore } from '@/js/store/publicStore';

const loadingShowed = ref(false)

const dialogShowed = ref(false)
const dialogTitle = ref('')
const dialogDesc = ref('')
const dialogType = ref('')
const dialogShowCancelBtn = ref(false)
const dialogDelete = ref(false)
let dialogAction = null

const emitter = inject('$emitter');
const store = publicStore();

onMounted(() => {
    // emitter.on('showToastMessage', showToastMessage);
    emitter.on('showDialog', showDialog);
    emitter.on('showLoading', showLoading);
})

onUnmounted(() => {
    emitter.off('showDialog', showDialog);
    emitter.off('showLoading', showLoading);
})

function showDialog(type, title, desc, action=null, del=false) {
    dialogAction = null;
    dialogType.value = type;
    dialogTitle.value = title;
    dialogDesc.value = desc;
    dialogShowed.value = true;
    dialogDelete.value = del;

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
    emitter.emit('focusElementError');
}

function showLoading(showed) {
    loadingShowed.value = showed;
}

</script>

<style scoped>
@import url(./the-content.css);
</style>