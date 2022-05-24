import {createRouter, createWebHashHistory, RouteRecordRaw} from 'vue-router'
import Settings from "@/components/settings/Settings.vue";
import Bikes from "@/components/bikes/Bikes.vue";
import Parts from "@/components/parts/Parts.vue";
import Register from "@/components/auth/Register.vue";
import Login from "@/components/auth/Login.vue";
import MainSite from "@/components/MainSite.vue";

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
