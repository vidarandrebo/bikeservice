<template>
    <header>
        <div class="menu-group">
            <router-link to="/"><h3>BikeHistory</h3></router-link>
        </div>
        <div class="menu-group">
            <router-link to="/bikes"><i class="fa fa-bicycle fa-2x"></i></router-link>
            <router-link to="/parts"><i class="fa fa-wrench fa-2x"></i></router-link>
        </div>
        <div class="menu-group">
            <p v-if="user" id="username">{{ user }}</p>
            <router-link v-if="user" to="/settings"><i class="fa fa-cog fa-2x"></i></router-link>
            <a v-if="user" href="#" v-on:click="logout"><i class="fa fa-sign-out fa-2x"></i></a>
            <router-link v-if="!user" to="/login"><i class="fa fa-sign-in fa-2x"></i></router-link>
            <router-link v-if="!user" to="/register"><i class="fa fa-user-plus fa-2x"></i></router-link>
        </div>
    </header>
</template>
<script lang="ts">
import {defineComponent, PropType} from 'vue';
import {httpGet} from "@/models/httpMethods";
import router from "@/router";

export default defineComponent({
    name: 'Menubar',
    props: {
        user: {
            type: String as PropType<string>,
        }
    },
    emits: {
        updateUsernameEvent(value: string) {
            return true
        },
    },
    methods: {
        logout: async function () {
            let response = await httpGet("/logout");
            if (response.status == 200) {
                this.$emit('updateUsernameEvent', "");
                await router.push("/login");
            }
        }
    }
})
</script>
<style scoped>
header h3 {
    display: inline;
}
header {
    height: 40px;
    background-color: #ff8c00;
    color: white;
    display: flex;
    justify-content: space-between;
}

a {
    color: white;
    text-decoration: none;
    margin-left: 10px;
    margin-right: 10px;
    border-radius: 5px;
    padding-left: 2px;
    padding-right: 2px;
}

.router-link-active {
    background-color: #ff6c00;
}
.menu-group {
    display: flex;
    align-items: center;
}


</style>