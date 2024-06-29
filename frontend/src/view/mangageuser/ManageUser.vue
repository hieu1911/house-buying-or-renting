<template>
    <div>
        <table class="table table--w-100">
            <thead class="table-head table-head__row">
                <th class="table-head__col col-center">STT</th>
                <th class="table-head__col">Tên</th>
                <th class="table-head__col">Email</th>
                <th class="table-head__col">Số điện thoại</th>
                <th class="table-head__col col-center">Quyền</th>
                <th class="table-head__col col-center">Xóa</th>
            </thead>
            <tbody  class="table-body content-data__body">
                <tr v-for="(item, index) in users" :key="index" class="table-body__row">
                    <td class="table-body__col col-center">{{ index + 1 }}</td>
                    <td class="table-body__col">{{ item.FullName }}</td>
                    <td class="table-body__col">{{ item.Email }}</td>
                    <td class="table-body__col">{{ item.PhoneNumber }}</td>
                    <td class="table-body__col col-center">
                       <v-button type="secondary"
                            :label="item.Role == 2 ? 'Xem và đăng' : 'Chỉ xem'"
                       ></v-button>
                    </td>
                    <td class="table-body__col col-center">
                        <v-button
                            label="Xóa"
                            type="delete"
                        ></v-button>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</template>

<script setup>
import { onBeforeMount, reactive } from 'vue';
import { getUserInfo } from '@/js/service/auth';
import { getRecords } from '@/js/service/base';
import router from '@/js/router/router';

const users = reactive([]);

onBeforeMount(async () => {
    const user = await getUserInfo();
    console.log(user.data);
    if (!user.data || user.data.Role != 1) {
        router.push('/login?returnUrl=manage-user');
        return;
    }

    users.splice(0, users.length);

    const userList = await getRecords('Auth');
    if (userList.data) {
        userList.data.forEach(u => {
            if (u.Role != 1) users.push(u);
        });
    }
});
</script>

<style scoped>
@import url(./manage-user.css);
</style>