import MainPage from "../components/MainPage.vue";
import BikesPage from "../components/bikes/Bikes.vue";
import PartsPage from "../components/parts/Parts.vue";
import RegisterPage from "../components/auth/Register.vue";
import LoginPage from "../components/auth/Login.vue";
import SettingsPage from "../components/settings/Settings.vue";
import AccountSettings from "../components/settings/AccountSettings.vue";
import BikeTypeSettings from "../components/settings/BikeTypeSettings.vue";
import PartTypeSettings from "../components/settings/PartTypeSettings.vue";
import { createRouter, createWebHistory, RouteRecordRaw } from "vue-router";

const routes: Array<RouteRecordRaw> = [
    {
        path: "/",
        name: "MainSite",
        component: MainPage
    },
    {
        path: "/bikes",
        name: "Bikes",
        component: BikesPage
    },
    {
        path: "/parts",
        name: "Parts",
        component: PartsPage
    },
    {
        path: "/register",
        name: "Register",
        props: true,
        component: RegisterPage
    },
    {
        path: "/login",
        name: "Login",
        props: true,
        component: LoginPage
    },
    {
        path: "/settings",
        name: "Settings",
        props: true,
        component: SettingsPage,
        children: [
            {
                name: "AccountSettings",
                path: "/settings/account",
                component: AccountSettings
            },
            {
                name: "BikeTypeSettings",
                path: "/settings/bike-types",
                component: BikeTypeSettings
            },
            {
                name: "Part",
                path: "/settings/part-types",
                component: PartTypeSettings
            }
        ]
    },
    {
        path: "/auth",
        name: "Course",
        props: true,
        component: RegisterPage
    },
    {
        path: "/:pathMatch(.*)*",
        redirect: "/"
    }
];

const router = createRouter({
    history: createWebHistory(),
    routes
});

export default router;
