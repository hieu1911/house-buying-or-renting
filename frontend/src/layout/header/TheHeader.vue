<template>
    <div class="header-wrapper">
        <div class="header-left">
            <v-icon type='logo'></v-icon>
            <RouterLink to="/">
                <h3>Real estate</h3>
            </RouterLink>
            <div class="header-link" v-if="!publicStore().isHomePage && !isAdmin">
                <a :class="{'active': !publicStore().isRenting}" @click="publicStore().setIsRenting(false)">{{ $t('header.buy') }}</a>
                <a :class="{'active': publicStore().isRenting}" @click="publicStore().setIsRenting(true)">{{ $t('header.rent') }}</a>
            </div>
            <div class="header-link" v-if="!publicStore().isHomePage && isAdmin">
                <a :class="{'active': currentRouteName.slice(1) == 'manage-user'}" href="/manage-user">Người dùng</a>
                <a :class="{'active': currentRouteName.slice(1) == 'manage-post'}" href="/manage-post">Bài đăng</a>
            </div>
        </div>
        <div v-if="!publicStore().isHomePage && !isAdmin" class="header-mid">
            <v-input
                v-model="searchValue"
                placeholder="Bất động sản"
                type="primary"
                w100
                hasIcon
                iconType="search"
                @clickIcon="handleSearch()"
            >
            </v-input>
            <v-button
                type="hasIconSecondary"
                :label="$t('home.filter')"
                icon="filter"
                @click="common.showFilter()"
            ></v-button>
        </div>
        <div class="header-right">
            <RouterLink to="/post" v-if="!isAdmin">
                <v-button
                    :label="$t('post.post')"
                    type="hasIconPrimary"
                    icon="checked"
                ></v-button>
            </RouterLink>
            <v-icon type='notify' v-if="!isAdmin"></v-icon>
            <v-icon type='user' @click="showMenu = true;" v-if="!userName"></v-icon>
            <div v-else class="avt" @click="showMenu = true;">{{ userName?.split(' ')[userName?.split(' ').length - 1][0] }}</div>
            <div v-show="showMenu" class="header-menu" ref="showMenuRef">
                <div v-for="(item, index) in menu" :key="index" @click="item.click(); showMenu = false">
                    <v-icon :type="item.icon"></v-icon>
                    <span>{{ item.title }}</span>
                </div>
            </div>
        </div>
    </div>
</template>

<script setup>
import { reactive, ref, onMounted, computed } from 'vue';
import { useRoute } from 'vue-router';
import { publicStore } from '@/js/store/publicStore';
import { useOnClickOutside } from '@/js/composable/click-outside';
import { getUserInfo, signout } from '@/js/service/auth';
import router from '@/js/router/router';
import common from '@/js/common/helper';

const route = useRoute()
const currentRouteName = computed(() => route.path)
const menu = reactive([]);
const showMenu = ref(false);
const showMenuRef = ref(null);
const searchValue = ref('');
const userName = ref('');
const isAdmin = ref(false);

onMounted(async () => {
    if (showMenuRef.value) {
        useOnClickOutside(showMenuRef.value, () => {
            showMenu.value = false;
        });
    }

    await getUser();
})

async function getUser() {
    menu.splice(0, menu.length)
    const user = await getUserInfo();
    if (user.data) {
        if (user.data.Role == 1) isAdmin.value = true;

        userName.value = user.data.FullName;
        if (user.data.Role == 1) {
            menu.push.apply(menu, [
                {
                    icon: 'logout',
                    title: 'Đăng xuất',
                    click: async () => { 
                        await signout(); 
                        window.location.reload();
                    }
                }
            ]);
        } else {
            menu.push.apply(menu, [
                {
                    icon: 'user-info',
                    title: 'Chỉnh sửa thông tin',
                    click: () => {}
                },
                {
                    icon: 'post-history',
                    title: 'Lịch sử đăng bài',
                    click: () => {
                        window.location.href = `/list?ownerId=${user.data.Id}`;
                    }
                },
                {
                    icon: 'post-saved',
                    title: 'Bài đăng đã lưu',
                    click: () => {
                        window.location.href = `/list?saved=true`;
                    }
                },
                {
                    icon: 'logout',
                    title: 'Đăng xuất',
                    click: async () => { 
                        await signout(); 
                        window.location.reload();
                    }
                }
            ]);
        }
        

    } else {
        userName.value = '';
        menu.push.apply(menu, [
            {
                icon: 'login',
                title: 'Đăng nhập',
                click: () => router.push(`/login${currentRouteName.value.slice(1) ? '?returnUrl=' + currentRouteName.value.slice(1) : ''}`)
            },
            {
                icon: 'register',
                title: 'Đăng ký',
                click: () => router.push('/register')
            }
        ]);
    }
}

function handleSearch() {
    window.location.href = `/list?search=${searchValue.value}&postType=${publicStore().isRenting ? 1 : 2}`;
}

</script>

<style scoped>
@import url(./the-header.css);
</style>