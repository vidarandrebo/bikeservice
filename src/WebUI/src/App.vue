<template>
    <MenuBar :user="user" @update-username-event="updateUsernameHandler"></MenuBar>
    <main>
        <!--Changes depending on which component is active-->
        <RouterView :user="user" @update-username-event="updateUsernameHandler"></RouterView>
    </main>
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
