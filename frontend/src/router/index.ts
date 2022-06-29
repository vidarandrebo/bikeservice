import {createRouter, createWebHashHistory, createWebHistory, RouteRecordRaw} from 'vue-router'
import Settings from "@/components/settings/Settings.vue";
import Bikes from "@/components/bikes/Bikes.vue";
import Parts from "@/components/parts/Parts.vue";
import Register from "@/components/auth/Register.vue";
import Login from "@/components/auth/Login.vue";
import MainSite from "@/components/MainSite.vue";
import AccountSettings from "@/components/settings/AccountSettings.vue";

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
        component: Settings,
        children:[
            {
                name: 'AccountSettings',
                path: '/settings/account',
                component: AccountSettings
            }
        ]
    },
    {
        path: '/auth',
        name: 'Course',
        props: true,
        component: Register
    },
    {
        path: '/:pathMatch(.*)*',
        redirect: '/'
    }
]

const router = createRouter({
    history: createWebHistory(process.env.BASE_URL),
    routes
})

export default router
