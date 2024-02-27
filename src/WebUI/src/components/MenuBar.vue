<template>
    <header class="flex justify-between">
        <div class="p-1">
            <RouterLink to="/">
                <FontAwesomeIcon icon="home" size="2x" title="Home"></FontAwesomeIcon>
            </RouterLink>
        </div>
        <div class="flex gap-4 p-1">
            <RouterLink to="/bikes">
                <FontAwesomeIcon icon="bicycle" size="2x" title="Bikes"></FontAwesomeIcon>
            </RouterLink>
            <RouterLink to="/parts">
                <FontAwesomeIcon icon="cog" size="2x" title="Parts"></FontAwesomeIcon>
            </RouterLink>
        </div>
        <div class="flex gap-4 p-1">
            <p v-if="user" id="username">{{ user }}</p>
            <RouterLink v-if="user" to="/settings">
                <FontAwesomeIcon icon="tools" size="2x" title="Settings"></FontAwesomeIcon>
            </RouterLink>
            <a v-if="user" href="#" @click="logout">
                <FontAwesomeIcon icon="sign-out" size="2x" title="Log out"></FontAwesomeIcon>
            </a>
            <RouterLink v-if="!user" to="/login">
                <FontAwesomeIcon icon="sign-in" size="2x" title="Login"></FontAwesomeIcon>
            </RouterLink>
            <RouterLink v-if="!user" to="/register">
                <FontAwesomeIcon icon="user-plus" size="2x" title="Register"></FontAwesomeIcon>
            </RouterLink>
        </div>
    </header>
</template>
<script lang="ts">
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
import { defineComponent, PropType } from "vue";
import router from "../router";

export default defineComponent({
    name: "MenuBar",
    components: {
        FontAwesomeIcon
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
