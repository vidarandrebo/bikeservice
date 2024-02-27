<template>
    <header>
        <div>
            <router-link to="/">
                <font-awesome-icon icon="home" size="2x" title="Home"></font-awesome-icon>
            </router-link>
        </div>
        <div>
            <router-link to="/bikes">
                <font-awesome-icon icon="bicycle" size="2x" title="Bikes"></font-awesome-icon>
            </router-link>
            <router-link to="/parts">
                <font-awesome-icon icon="cog" size="2x" title="Parts"></font-awesome-icon>
            </router-link>
        </div>
        <div>
            <p v-if="user" id="username">{{ user }}</p>
            <router-link v-if="user" to="/settings">
                <font-awesome-icon icon="tools" size="2x" title="Settings"></font-awesome-icon>
            </router-link>
            <a v-if="user" href="#" @click="logout">
                <font-awesome-icon icon="sign-out" size="2x" title="Log out"></font-awesome-icon>
            </a>
            <router-link v-if="!user" to="/login">
                <font-awesome-icon icon="sign-in" size="2x" title="Login"></font-awesome-icon>
            </router-link>
            <router-link v-if="!user" to="/register">
                <font-awesome-icon icon="user-plus" size="2x" title="Register"></font-awesome-icon>
            </router-link>
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
