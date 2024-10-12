<template>
    <header class="flex justify-between bg-blue-500 text-white">
        <div class="flex gap-4 px-4">
            <MenuBarRouterLink to="/">
                <MenuBarIcon icon="home" title="Home"></MenuBarIcon>
            </MenuBarRouterLink>
            <MenuBarRouterLink to="/bikes">
                <MenuBarIcon icon="bicycle" title="Bikes"></MenuBarIcon>
            </MenuBarRouterLink>
            <MenuBarRouterLink to="/parts">
                <MenuBarIcon icon="cog" title="Parts"></MenuBarIcon>
            </MenuBarRouterLink>
        </div>
        <div class="flex gap-4 px-4">
            <MenuBarRouterLink v-if="user.email" to="/settings">
                <MenuBarIcon icon="user" title="Settings"></MenuBarIcon>
            </MenuBarRouterLink>
            <a v-if="user.email" href="#" @click="logout">
                <MenuBarIcon icon="sign-out" title="Log out"></MenuBarIcon>
            </a>
            <MenuBarRouterLink v-if="!user.email" to="/login">
                <MenuBarIcon icon="sign-in" title="Login"></MenuBarIcon>
            </MenuBarRouterLink>
            <MenuBarRouterLink v-if="!user.email" to="/register">
                <MenuBarIcon icon="user-plus" title="Register"></MenuBarIcon>
            </MenuBarRouterLink>
        </div>
    </header>
</template>
<script setup lang="ts">
import router from "../Router";
import MenuBarIcon from "./Common/MenuBarIcon.vue";
import { inject } from "vue";
import { DefaultUserDependency, User } from "../Models/Auth/User.ts";
import MenuBarRouterLink from "./Common/MenuBarRouterLink.vue";

const { user, setUser } = inject("user", DefaultUserDependency, true);

async function logout() {
    user.value.removeFromLocalStorage();
    setUser(new User());
    await router.push("/login");
}
</script>
