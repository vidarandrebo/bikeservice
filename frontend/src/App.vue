<template>
    <header>
        <menubar v-bind:user="user" @updateUsername="updateUsername"></menubar>
    </header>
    <main>
        <!--Changes depending on which component is active-->
        <router-view v-bind:user="user" @updateUsername="updateUsername"></router-view>
    </main>
    <div class="space"></div>
    <footer>
        <a href="https://github.com/vidarandrebo">https://github.com/vidarandrebo</a>
    </footer>
</template>

<script lang="ts">
import Menubar from './components/Menu.vue';
import MainSite from './components/MainSite.vue';
import {defineComponent} from 'vue';
import {getWithBody} from "@/models/genericFetch";
import {AuthRouteResponse} from "@/models/auth/authRouteResponse";

export default defineComponent({
    name: 'App',
    components: {
        Menubar,
        MainSite,
    },
    data: function () {
        return {
            user: null as any,
        }
    },
    methods: {
        updateUsername: function (name: string): void {
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
