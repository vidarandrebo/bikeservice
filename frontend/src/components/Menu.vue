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
import {get} from "@/models/genericFetch";
import router from "@/router";

export default defineComponent({
    props: {
        user: {
            type: Object as PropType<string>,
        }
    },
    name: 'Menubar',
    emits: ['updateUsername'],
    methods: {
        logout: async function () {
            let response = await get("/logout");
            if (response.status == 200) {
                this.$emit('updateUsername', "");
                await router.push("/login");
            }
        }
    }
})
</script>
