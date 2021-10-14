import { createRouter, createWebHashHistory } from 'vue-router'
import MainSite from '../components/mainsite.vue'
import Login from '../components/login.vue'
import Register from '../components/register.vue'
import Bike from '../components/bike.vue'

const routes = [
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
  history: createWebHashHistory(),
  routes
})

export default router
