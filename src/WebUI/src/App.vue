<template>
    <MenuBar :user="user" @update-username-event="updateUsernameHandler"></MenuBar>
    <RouterView
        :user="user" class="flex flex-row justify-center items-center"
        @update-username-event="updateUsernameHandler"></RouterView>
    <PageFooter></PageFooter>
</template>


<script lang="ts">
import MenuBar from "./components/MenuBar.vue";
import { defineComponent } from "vue";
import { AuthRouteResponse } from "./models/auth/authRouteResponse.ts";
import { httpGetWithBody } from "./models/httpMethods.ts";
import PageFooter from "./components/PageFooter.vue";

export default defineComponent({
    name: "App",
    components: {
        PageFooter,
        MenuBar
    },
    data: function() {
        return {
            user: ""
        };
    },
    created: async function() {
        let result = await httpGetWithBody<AuthRouteResponse>("/api/login");
        this.user = result.body.userName;
    },
    methods: {
        updateUsernameHandler: function(name: string): void {
            this.user = name;
        }
    }
});
</script>
