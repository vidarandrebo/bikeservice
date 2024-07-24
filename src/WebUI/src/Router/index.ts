import MainPage from "../Components/MainPage.vue";
import BikesPage from "../Components/Bikes/BikesPage.vue";
import PartsPage from "../Components/Parts/PartsPage.vue";
import RegisterPage from "../Components/Auth/RegisterPage.vue";
import LoginPage from "../Components/Auth/LoginPage.vue";
import SettingsPage from "../Components/Settings/SettingsPage.vue";
import AccountSettings from "../Components/Settings/AccountSettings.vue";
import BikeTypeSettings from "../Components/Settings/BikeTypeSettings.vue";
import PartTypeSettings from "../Components/Settings/PartTypeSettings.vue";
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
router.beforeEach((to, _, next) => {
    document.title = "BikeService - " + <string>to.name;
    next();
});

export default router;
