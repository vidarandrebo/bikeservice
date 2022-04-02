import {createRouter, createWebHashHistory, RouteRecordRaw} from 'vue-router'
import Settings from "@/components/settings.vue";
import Bikes from "@/components/bikes.vue";
import Parts from "@/components/parts.vue";
import Register from "@/components/register.vue";
import Login from "@/components/login.vue";
import MainSite from "@/components/mainsite.vue";

const routes: Array<RouteRecordRaw> = [
    {
        path: '/',
        name: 'MainSite',
        component: MainSite
    },
    {
        path: '/bikes',
        name: 'Bikes',
        component: Bikes
    },
    {
        path: '/parts',
        name: 'Parts',
        component: Parts
    },
    {
        path: '/register',
        name: 'Register',
        props: true,
        component: Register
    },
    {
        path: '/login',
        name: 'Login',
        props: true,
        component: Login
    },
    {
        path: '/settings',
        name: 'Settings',
        props: true,
        component: Settings
    },
    {
        path: '/auth',
        name: 'Course',
        props: true,
        component: Register
    }
]

const router = createRouter({
    history: createWebHashHistory(process.env.BASE_URL),
    routes
})

export default router
