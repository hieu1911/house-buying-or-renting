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
        <div v-if="!publicStore().isAuthPage && !isAdmin" class="message">
            <img src="../../assets/icon/messenger-main.png" @click="showMessage()"/>
            <div class="message-rect" v-if="showMessageRect && receiverId" ref="messageRectRef">
                <div class="receiver-info">
                    <span>{{ receiver.FullName?.split(' ')[receiver.FullName?.split(' ').length - 1][0] }}</span>
                    <h4>{{ receiver.FullName }}</h4>
                    <div>
                        <v-icon type="next-big" @click="receiverId=''"></v-icon>
                    </div>
                </div>
                <div class="message-body" ref="messageBodyRef">
                    <div v-for="(item, index) in messages" :key="index" >
                        <span :class="{'sender': item.SenderId == user.Id}">
                            {{ item.Content }}
                        </span>
                    </div>
                </div>
                <div class="message-input">
                    <v-input
                        placeholder="Nhập nội dung..."
                        w100
                        hasIcon
                        iconType="send"
                        iconAfter
                        @clickIcon="sendMessage"
                        v-model="messageContent"
                    ></v-input>
                </div>
            </div>
            <div v-if="showMessageRect && !receiverId" class="message-rect" ref="messageRectRef">
                <div class="title">
                    <h4>Tin nhắn</h4>
                    <v-icon type="close" @click="showMessageRect = false" desc="Đóng"></v-icon>
                </div>
                <div>
                    <div v-for="(item, index) in contacts" :key="index" class="message-contact" @click="receiverId = item.Id; showMessage()">
                        <span class="avt">{{ item.FullName?.split(' ')[item.FullName?.split(' ').length - 1][0] }}</span>
                        <span class="user-name">{{ item.FullName }}</span>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script setup>
import { onMounted, onUnmounted, inject, reactive, computed, nextTick } from 'vue';
import { useRoute } from 'vue-router';
import { ref } from 'vue';
import { useSignalR } from '@dreamonkey/vue-signalr';

import { publicStore } from '@/js/store/publicStore';
import { getUserInfo } from '@/js/service/auth';
import { getRecord, createRecord } from '@/js/service/base';
import { getByUser, getContacts } from '@/js/service/message';
import FilterCard from '@/view/components/FilterCard/FilterCard.vue'
import router from '@/js/router/router';
import { useOnClickOutside } from '@/js/composable/click-outside';
// import { useChat } from '@/js/firebase/chat';

const route = useRoute()
const currentRouteName = computed(() => route.path)
const loadingShowed = ref(false)
const showFilter = ref(false);

const dialogShowed = ref(false);
const dialogTitle = ref('');
const dialogDesc = ref('');
const dialogType = ref('');
const dialogShowCancelBtn = ref(false);
const toastMessages = reactive([]);
const topToastMessages = ref('top: 20px');
const user = reactive({});
const isLogin = ref(false);
const showMessageRect = ref(false);
const messageRectRef = ref(null);
const receiverId = ref('');
const receiver = reactive({});
let dialogAction = null
const signalr = useSignalR();
const messageContent = ref('');
const messages = reactive([]);
const contacts = reactive([]);
const messageBodyRef = ref(null);

const emitter = inject('$emitter');
const store = publicStore();
const isAdmin = ref(false);

onMounted(async () => {
    const userData = await getUserInfo();
    if (userData && userData.data && userData.data.Role == 1) isAdmin.value = true;

    emitter.on('showDialog', showDialog);
    emitter.on('showLoading', showLoading);
    emitter.on('showToastMessage', showToastMessage);
    emitter.on('showFilter', () => showFilter.value = true);
    emitter.on('showMessageWithReceiver', showMessageWithReceiver);
    window.addEventListener("scroll", onScroll);

    if (messageRectRef.value) {
        useOnClickOutside(messageRectRef.value, () => {
            showMessageRect.value = false;
        });
    }

    signalr.on("ReceivedMessage", (senderId, receiverId, content) => {
        messages.push({
            SenderId: senderId,
            ReceiverId: receiverId,
            Content: content
        });

        nextTick(() => {
            if (messageBodyRef.value) {
                messageBodyRef.value.scrollTop = messageBodyRef.value.scrollHeight;
            }
        });  
    })
});

onUnmounted(() => {
    emitter.off('showDialog', showDialog);
    emitter.off('showLoading', showLoading);
    emitter.off('showToastMessage', showToastMessage);
    emitter.off('showFilter', showFilter.value = true);
    emitter.off('showMessageWithReceiver', showMessageWithReceiver);
    window.removeEventListener("scroll", onScroll);
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

async function showMessage() {
    const userData = await getUserInfo();
    if (userData.data) {
        Object.assign(user, userData.data);
        isLogin.value = true;
    } else {
        Object.assign(user, {});
        isLogin.value = false;
    }

    if (isLogin.value) {
        showMessageRect.value = true;
        signalr.invoke("AddToGroupAsync", user.Id);

        if (receiverId.value) {
            const receiverInfo = await getRecord('Auth', receiverId.value)
            if (receiverInfo.data) {
                Object.assign(receiver, receiverInfo.data);

                const messagesList = await getByUser(user.Id, receiverId.value);
                if (messagesList.data) {
                    Object.assign(messages, messagesList.data);
                }
            }
        } else {
            const contactList = await getContacts(user.Id);
            if (contactList.data) {
                Object.assign(contacts, contactList.data);
            }
        }
    } else {
        router.push(`/login${currentRouteName.value.slice(1) ? '?returnUrl=' + currentRouteName.value.slice(1) : ''}`)
    }
}

function showMessageWithReceiver(id, content) {
    receiverId.value = id;
    console.log(content);
    messageContent.value = content;
    showMessage();
}

async function sendMessage() {
    if (messageContent.value.trim()) {
        await createRecord("Message", {
            SenderId: user.Id,
            ReceiverId: receiverId.value,
            Content: messageContent.value
        });

        signalr.invoke("SendMessage", user.Id, receiverId.value, messageContent.value);
        messageContent.value = '';
    }
}

</script>

<style scoped>
@import url(./the-content.css);
</style>