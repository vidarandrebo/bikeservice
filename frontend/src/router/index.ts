import {RouteRecordRaw, createRouter, createWebHistory} from 'vue-router'
import MainSite from '../components/mainsite.vue'
import Login from '../components/login.vue'
import Register from '../components/register.vue'
import Bike from '../components/bike.vue'

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
    path: '/login',
    name: 'Login',
    props: true,
    component: Login
  },
  {
    path: '/register',
    name: 'Course',
    props: true,
    component: Register
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
