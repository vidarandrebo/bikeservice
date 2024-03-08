<template>
    <header class="flex justify-between bg-blue-500 text-white">
        <div class="px-1">
            <RouterLink to="/">
                <MenuBarIcon icon="home" title="Home"></MenuBarIcon>
            </RouterLink>
        </div>
        <div class="flex gap-4 px-1">
            <RouterLink to="/bikes">
                <MenuBarIcon icon="bicycle" title="Bikes"></MenuBarIcon>
            </RouterLink>
            <RouterLink to="/parts">
                <MenuBarIcon icon="cog" title="Parts"></MenuBarIcon>
            </RouterLink>
        </div>
        <div class="flex gap-4 px-1">
            <p v-if="user" id="username" class="h-full flex flex-col justify-center">{{ user }}</p>
            <RouterLink v-if="user" to="/settings">
                <MenuBarIcon icon="tools" title="Settings"></MenuBarIcon>
            </RouterLink>
            <a v-if="user" href="#" @click="logout">
                <MenuBarIcon icon="sign-out" title="Log out"></MenuBarIcon>
            </a>
            <RouterLink v-if="!user" to="/login">
                <MenuBarIcon icon="sign-in" title="Login"></MenuBarIcon>
            </RouterLink>
            <RouterLink v-if="!user" to="/register">
                <MenuBarIcon icon="user-plus" title="Register"></MenuBarIcon>
            </RouterLink>
        </div>
    </header>
</template>
<script lang="ts">
import { defineComponent, PropType } from "vue";
import router from "../router";
import MenuBarIcon from "./common/MenuBarIcon.vue";

export default defineComponent({
    name: "MenuBar",
    components: {
        MenuBarIcon
    },
    props: {
        user: {
            type: String as PropType<string>,
            default: ""
        }
    },
    emits: {
        updateUsernameEvent(value: string) {
            return value;
        }
    },
    methods: {
        logout: async function () {
            localStorage.removeItem("authToken");
            this.$emit("updateUsernameEvent", "");
            await router.push("/login");
        }
    }
});
</script>
