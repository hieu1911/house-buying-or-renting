<template>
    <div class="user-info">
        <div class="user-info-input">
            <h4>Chỉnh sửa thông tin</h4>
            <v-input
                label="Họ tên"
                w100
                v-model="fullName"
            ></v-input>
            <v-input
                label="Email"
                w100
                v-model="email"
            ></v-input>
            <v-input
                label="Số điện thoại"
                w100
                v-model="phoneNumber"
            ></v-input>
        </div>
        <hr/>
        <div class="user-info-btn">
            <v-button
                label="Thoát"
                type="secondary"
                @click="emitter.emit('closeUserInfo')"
            ></v-button>
            <v-button
                label="Lưu"
                type="primary"
                @click="saveUserInfo()"
            ></v-button>
        </div>
    </div>
</template>

<script setup>
import emitter from 'tiny-emitter/instance';
import { getUserInfo } from '@/js/service/auth';
import { onMounted, reactive, ref } from 'vue';
import common from '@/js/common/helper';
import enums from '@/js/common/enum';
import { updateRecord } from '@/js/service/base';

const user = reactive({});
const fullName = ref('');
const email = ref('');
const phoneNumber = ref('');

onMounted(async () => {
    const userInfo = await getUserInfo();
    if (userInfo.data) {
        Object.assign(user, userInfo.data);
        fullName.value = user.FullName;
        email.value = user.Email;
        phoneNumber.value = user.PhoneNumber;
    }
})

async function saveUserInfo() {
    if (!fullName.value || !email.value || !phoneNumber.value) {
        common.showDialog(enums.statusEnum.WARNING, 'Cảnh báo', ["Vui lòng nhập đủ thông tin!"])
    } else {
        user.FullName = fullName.value;
        user.Email = email.value;
        user.PhoneNumber = phoneNumber.value
        await updateRecord('Auth', user.Id, {
            ...user
        });

        emitter.emit('closeUserInfo');
        common.showToastMessage("success", "Thành công!", "Cập nhật thông tin thành công.")
    }
}

</script>

<style scoped>
@import url(./user-info.css);
</style>