<template>
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
