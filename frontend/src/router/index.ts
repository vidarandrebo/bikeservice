import {createRouter, createWebHashHistory, RouteRecordRaw} from 'vue-router'
import MainSite from '../components/mainsite.vue'
import Login from '../components/login.vue'
import Register from '../components/register.vue'

const routes: Array<RouteRecordRaw> = [
    {
        path: '/',
        name: 'MainSite',
        component: MainSite
    },
    {
        path: '/global',
        name: 'Global',
        component: MainSite
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
