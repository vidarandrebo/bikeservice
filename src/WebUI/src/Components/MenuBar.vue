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
            <p v-if="user.username" id="username" class="h-full flex flex-col justify-center">{{ user.username }}</p>
            <RouterLink v-if="user.username" to="/settings">
                <MenuBarIcon icon="tools" title="Settings"></MenuBarIcon>
            </RouterLink>
            <a v-if="user.username" href="#" @click="logout">
                <MenuBarIcon icon="sign-out" title="Log out"></MenuBarIcon>
            </a>
            <RouterLink v-if="!user.username" to="/login">
                <MenuBarIcon icon="sign-in" title="Login"></MenuBarIcon>
            </RouterLink>
            <RouterLink v-if="!user.username" to="/register">
                <MenuBarIcon icon="user-plus" title="Register"></MenuBarIcon>
            </RouterLink>
        </div>
    </header>
</template>
<script setup lang="ts">
import router from "../Router";
import MenuBarIcon from "./Common/MenuBarIcon.vue";
import { inject } from "vue";
import { DefaultUserDependency, User } from "../Models/Auth/User.ts";

const { user, setUser } = inject("user", DefaultUserDependency, true);

async function logout() {
    localStorage.removeItem("authToken");
    setUser(new User(""));
    await router.push("/login");
}
</script>
