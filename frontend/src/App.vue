<template>
    <header>
        <menubar v-bind:user="user" @fetchUsername="setUser"></menubar>
    </header>
    <main>
        <!--Changes depending on which component is active-->
        <router-view v-bind:user="user" @fetchUsername="setUser"></router-view>
    </main>
    <div class="space"></div>
    <footer>
        <a href="https://github.com/vidarandrebo">https://github.com/vidarandrebo</a>
    </footer>
</template>

<script lang="ts">
import Menubar from './components/menu.vue';
import MainSite from './components/mainsite.vue';
import {defineComponent} from 'vue';
import {getWithBody} from "@/models/genericFetch";
import {AuthRouteResponse} from "@/models/auth/authRouteResponse";

export default defineComponent({
    name: 'App',
    components: {
        Menubar,
        MainSite,
    },
    emits: ['fetchUsername'],
    data: function () {
        return {
            user: null as any,
        }
    },
    methods: {
        setUser: function (name: string): void {
            this.user = name;
        }
    },
    async created() {
        let result = await getWithBody<AuthRouteResponse>("/login");
        this.user = result.body.userName;
    }
})
</script>
<style>
@import "css/style.css";
</style>
