<template>
    <menu-bar :user="user" @update-username-event="updateUsernameHandler"></menu-bar>
    <main>
        <!--Changes depending on which component is active-->
        <router-view :user="user" @update-username-event="updateUsernameHandler"></router-view>
    </main>
    <div class="space"></div>
    <footer>
        <a href="https://github.com/vidarandrebo">https://github.com/vidarandrebo</a>
    </footer>
</template>

<script lang="ts">
import MenuBar from "./components/MenuBar.vue";
import { defineComponent } from "vue";
import { AuthRouteResponse } from "./models/auth/authRouteResponse.ts";
import { httpGetWithBody } from "./models/httpMethods.ts";

export default defineComponent({
    name: "App",
    components: {
        MenuBar
    },
    data: function () {
        return {
            user: ""
        };
    },
    created: async function () {
        let result = await httpGetWithBody<AuthRouteResponse>("/api/login");
        this.user = result.body.userName;
    },
    methods: {
        updateUsernameHandler: function (name: string): void {
            this.user = name;
        }
    }
});
</script>
<style>
:root {
    --card-color: #ffffff;
    --highlight-color: #ccc;
    --text-color: #000000;
    --white-text-color: #fff;
    --bg-color: #f4f1de;
    --accent-color: #495867;
    --accent-highlight-color: #577399;
    --error-color: #fe5f55;
}

/*sticky footer etc*/
html {
    height: 100%;
    background-color: var(--bg-color);
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
    background-color: var(--accent-color);
    margin-bottom: 0;
    color: var(--white-text-color);
    display: flex;
    align-items: center;
}

footer a {
    color: var(--white-text-color);
    margin-left: auto;
    margin-right: auto;
}

.card {
    background-color: var(--card-color);
    margin: 0.5rem;
    padding: 1rem;
    border-radius: 1rem;
}
</style>
