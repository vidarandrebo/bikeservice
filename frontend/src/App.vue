<template>
    <menubar v-bind:user="user" @updateUsernameEvent="updateUsernameHandler"></menubar>
    <main>
        <!--Changes depending on which component is active-->
        <router-view v-bind:user="user" @updateUsernameEvent="updateUsernameHandler"></router-view>
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
import {httpGetWithBody} from "@/models/httpMethods";
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
        updateUsernameHandler: function (name: string): void {
            this.user = name;
        }
    },
    created: async function () {
        let result = await httpGetWithBody<AuthRouteResponse>("/login");
        this.user = result.body.userName;
    }
})
</script>
<style>
/*sticky footer etc*/
html {
    height: 100%;
}

#app {
    display: flex;
    flex-direction: column;
    min-height: 100%;
    margin: 0;
}

body {
    height: 100%;
    margin: 0;
}


main {
    width: 100%;
    margin-left: auto;
    margin-right: auto;
    overflow: hidden;
}

.space {
    flex: 1;
}

footer {
    height: 60px;
    background-color: #ff8c00;
    margin-bottom: 0;
    color: white;
    display: flex;
    align-items: center;
}

footer a {
    color: white;
    margin-left: auto;
    margin-right: auto;
}


</style>
