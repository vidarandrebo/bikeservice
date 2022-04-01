<template>
    <div class="menugroup_left menugroup">
        <h3>BikeHistory</h3>
    </div>
    <div class="menugroup_middle menugroup">
        <router-link to="/"><i class="fa fa-bicycle fa-2x"></i></router-link>
        <router-link to="/global"><i class="fa fa-wrench fa-2x"></i></router-link>
    </div>
    <div class="menugroup_right menugroup">
        <p v-if="user" id="username">{{ user }}</p>
        <router-link v-if="user" to="/settings"><i class="fa fa-cog fa-2x"></i></router-link>
        <a v-if="user" href="#" v-on:click="logout"><i class="fa fa-sign-out fa-2x"></i></a>
        <router-link v-if="!user" to="/login"><i class="fa fa-sign-in fa-2x"></i></router-link>
        <router-link v-if="!user" to="/register"><i class="fa fa-user-plus fa-2x"></i></router-link>
    </div>
</template>
<script lang="ts">
import {defineComponent} from 'vue';
import router from '../router'
import {get} from "@/models/genericFetch";

export default defineComponent({
    props: ["user"],
    name: 'Menubar',
    emits: ['fetchUsername'],
    methods: {
        logout: async function () {
            let response = await get("/logout");
            if (response.status == 200) {
                this.$emit('fetchUsername', "");
                await router.push("/login");
            }
        }
    }
})
</script>
