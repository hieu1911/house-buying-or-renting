<template>
    <div class="header-wrapper">
        <div class="header-left">
            <v-icon type='logo'></v-icon>
            <h3>Real estate</h3>
            <div class="header-link">
                <a>{{ $t('header.buy') }}</a>
                <a>{{ $t('header.rent') }}</a>
            </div>
        </div>
        <div v-if="!publicStore().isHomePage" class="header-mid">
            <v-input
                placeholder="Bất động sản"
                type="primary"
                w100
                hasIcon
                iconType="search"
            >
            </v-input>
            <v-button
                type="hasIconSecondary"
                :label="$t('home.filter')"
                icon="filter"
            ></v-button>
        </div>
        <div class="header-right">
            <v-button
                :label="$t('post.post')"
                type="hasIconPrimary"
                icon="checked"
            ></v-button>
            <v-icon type='notify'></v-icon>
            <v-icon type='user' @click="showUserMenu()"></v-icon>
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
import { getUserInfo } from '@/js/service/auth';
import router from '@/js/router/router';

const route = useRoute()
const currentRouteName = computed(() => route.path)
const menu = reactive([]);
const showMenu = ref(false);
const showMenuRef = ref(null);
// const returnUrl = ref("");

onMounted(() => {
    if (showMenuRef.value) {
        useOnClickOutside(showMenuRef.value, () => {
            showMenu.value = false;
        });
    }
})

async function showUserMenu() {
    menu.splice(0, menu.length)
    const user = await getUserInfo();
  
    if (user.data) {
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
                click: () => {}
            }
        ]);
    } else {
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

    showMenu.value = true;
}

</script>

<style scoped>
@import url(./the-header.css);
</style>