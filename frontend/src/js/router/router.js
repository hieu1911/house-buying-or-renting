import { createRouter, createWebHistory } from 'vue-router';
import HomeView from '@/view/home/HomeView.vue';
import LoginView from '@/view/login/LoginView.vue';
import RegisterView from '@/view/register/RegisterView.vue';


const routes = [
    { path: '/', name: 'HomeRouter', component: HomeView },
    { path: '/login', name: 'LoginRouter', component: LoginView },
    { path: '/register', name: 'RegisterRouter', component: RegisterView}
];

const router = createRouter({
    history: createWebHistory(),
    routes
});

// router.beforeEach(async (to, from, next) => {
//     if (to.path == '/login') {
//         const user = await getUserInfo();
//         if (user) {
//             const navItems = JSON.parse(localStorage.getItem('navItems'));
//             const itemActive = navItems.filter(item => item.active)[0];
    
//             next({ path: itemActive.link});
//         } else {
//             next();
//         }
//     } else {
//         next();
//     }
// });

export default router;