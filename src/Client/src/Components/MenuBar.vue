<template>
    <header>
        <BNavbar shadow>
            <template #start>
                <BNavbarItem tag="router-link" to="/">
                    <FontAwesomeIcon icon="home" size="2x" title="Home"></FontAwesomeIcon>
                </BNavbarItem>
                <BNavbarItem v-if="user.email" tag="router-link" to="/bikes">
                    <FontAwesomeIcon icon="bicycle" size="2x"></FontAwesomeIcon>
                </BNavbarItem>
                <BNavbarItem v-if="user.email" tag="router-link" to="/parts">
                    <FontAwesomeIcon icon="cog" size="2x"></FontAwesomeIcon>
                </BNavbarItem>
            </template>
            <template #end>
                <BNavbarItem v-if="user.email" tag="router-link" to="/settings">
                    <FontAwesomeIcon icon="user" size="2x"></FontAwesomeIcon>
                </BNavbarItem>
                <BNavbarItem v-if="user.email" href="#" @click="logout">
                    <FontAwesomeIcon icon="sign-out" size="2x"></FontAwesomeIcon>
                </BNavbarItem>
                <BNavbarItem v-if="!user.email" tag="router-link" to="/login">
                    <FontAwesomeIcon icon="sign-in" size="2x"></FontAwesomeIcon>
                </BNavbarItem>
                <BNavbarItem v-if="!user.email" tag="router-link" to="/register">
                    <FontAwesomeIcon icon="user-plus" size="2x"></FontAwesomeIcon>
                </BNavbarItem>
            </template>
        </BNavbar>
    </header>
</template>
<script setup lang="ts">
import router from "../Router";
import { inject } from "vue";
import { DefaultUserDependency, User } from "../Models/Auth/User.ts";
import { BNavbar, BNavbarItem } from "buefy";
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";

const { user, setUser } = inject("user", DefaultUserDependency, true);

async function logout() {
    user.value.removeFromLocalStorage();
    setUser(new User());
    await router.push("/login");
}
</script>
