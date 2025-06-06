import { createApp } from "vue";
import App from "./App.vue";
import "./style.css";
import router from "./Router";
import { library } from "@fortawesome/fontawesome-svg-core";
import {
    faBicycle,
    faCog,
    faHome,
    faSignIn,
    faSignOut,
    faTools,
    faUserPlus,
    faUser
} from "@fortawesome/free-solid-svg-icons";

library.add(faHome, faCog, faUserPlus, faBicycle, faTools, faSignOut, faSignIn, faUser);

createApp(App).use(router).mount("#app");
