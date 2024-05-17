import { createRouter, createWebHistory } from 'vue-router';
import HomeView from '@/view/home/HomeView.vue';
import LoginView from '@/view/login/LoginView.vue';
import RegisterView from '@/view/register/RegisterView.vue';
import PostView from '@/view/post/PostView.vue';

const routes = [
    { path: '/', name: 'HomeRouter', component: HomeView },
    { path: '/login', name: 'LoginRouter', component: LoginView },
    { path: '/register', name: 'RegisterRouter', component: RegisterView},
    { path: '/post', name: 'PostView', component: PostView}
];

export const router = createRouter({
    history: createWebHistory(),
    routes
});

export default router;