<template>
    <div class="flex flex-col h-screen">
        <MenuBar :user="user" @update-username-event="updateUsernameHandler"></MenuBar>
        <RouterView
            :user="user"
            class="grow flex flex-row justify-center"
            @update-username-event="updateUsernameHandler"
        ></RouterView>
        <PageFooter></PageFooter>
    </div>
</template>

<script lang="ts">
import MenuBar from "./Components/MenuBar.vue";
import { defineComponent } from "vue";
import { AuthRouteResponse } from "./Models/Auth/AuthRouteResponse.ts";
import { httpGetWithBody } from "./Models/HttpMethods.ts";
import PageFooter from "./Components/PageFooter.vue";

export default defineComponent({
    name: "App",
    components: {
        PageFooter,
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
