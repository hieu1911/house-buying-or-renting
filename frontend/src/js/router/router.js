import { createRouter, createWebHistory } from 'vue-router';
import HomeView from '@/view/home/HomeView.vue';
import LoginView from '@/view/login/LoginView.vue';
import RegisterView from '@/view/register/RegisterView.vue';
import PostView from '@/view/post/PostView.vue';
import ListView from '@/view/list/ListView.vue';
import DetailView from '@/view/detail/DetailView.vue';

import { publicStore } from '../store/publicStore';

const routes = [
    { path: '/', name: 'HomeRouter', component: HomeView },
    { path: '/login', name: 'LoginRouter', component: LoginView },
    { path: '/register', name: 'RegisterRouter', component: RegisterView},
    { path: '/post', name: 'PostView', component: PostView},
    { path: '/list', name: 'ListView', component: ListView},
    { path: '/detail/:id', name: 'DetailView', component: DetailView}
];

export const router = createRouter({
    history: createWebHistory(),
    routes
});

router.beforeEach(() => {
    publicStore().setIsHomePage(false);
});

export default router;