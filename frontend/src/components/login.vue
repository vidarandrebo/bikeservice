<template>
    <div class="logregdiv">
        <form id="login" method="POST" v-on:submit.prevent="loginUser">
            <p v-for="err in loginData.errors">{{ err }}</p>
            <label for="username">Username</label>
            <input type="text" id="username" name="username" v-model="loginData.username" required>
            <label for="passwd">Password</label>
            <input type="password" id="passwd" name="passwd" v-model="loginData.passwd" required>
            <input type="submit" value="Login">
        </form>
    </div>
</template>
<script lang="ts">
import {defineComponent} from 'vue';
import {ILoginData, LoginData} from "@/models/auth/login";
import {IUser, User} from "@/models/auth/user";
import router from "@/router";

export default defineComponent({
    name: 'Login',
    data: function () {
        return {
            loginData: new LoginData() as ILoginData,
        }
    },
    emits: ['fetchUsername'],
    methods: {
        loginUser: async function (): Promise<void> {
            let user: IUser = new User(
                this.loginData.username,
                this.loginData.passwd
            );
            let response = await user.loginUserRequest();
            if (response.status == 200) {
                this.$emit('fetchUsername', response.body.userName);
                router.push('/')
            } else {
                this.loginData.errors = response.body.errors;
            }


        },
    }
})
</script>
