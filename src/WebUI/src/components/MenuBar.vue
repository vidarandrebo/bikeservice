<template>
    <header>
        <div class="menu-group">
            <router-link to="/">
                <font-awesome-icon icon="home" size="2x" title="Home"></font-awesome-icon>
            </router-link>
        </div>
        <div class="menu-group">
            <router-link to="/bikes">
                <font-awesome-icon icon="bicycle" size="2x" title="Bikes"></font-awesome-icon>
            </router-link>
            <router-link to="/parts">
                <font-awesome-icon icon="cog" size="2x" title="Parts"></font-awesome-icon>
            </router-link>
        </div>
        <div class="menu-group">
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
<style scoped>
header h3 {
    display: inline;
}

header {
    height: 40px;
    background-color: var(--accent-color);
    color: var(--white-text-color);
    display: flex;
    justify-content: space-between;
}

a {
    color: var(--white-text-color);
    text-decoration: none;
    margin-left: 10px;
    margin-right: 10px;
    border-radius: 5px;
    padding-left: 2px;
    padding-right: 2px;
}

.router-link-active {
    background-color: var(--accent-highlight-color);
}

.menu-group {
    display: flex;
    align-items: center;
}
</style>
