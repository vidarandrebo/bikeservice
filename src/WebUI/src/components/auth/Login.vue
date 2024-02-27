<template>
    <div>
        <form id="login" method="POST" @submit.prevent="loginUser">
            <p v-for="(err, i) in loginData.errors" :key="i">{{ err }}</p>
            <label for="username">Username</label>
            <input id="username" v-model="loginData.username" type="text" name="username" required />
            <label for="passwd">Password</label>
            <input id="passwd" v-model="loginData.passwd" type="password" name="passwd" required />
            <input type="submit" value="Login" />
        </form>
    </div>
</template>
<script lang="ts">
import { defineComponent } from "vue";
import { LoginData } from "../../models/auth/login.ts";
import { IUser, User } from "../../models/auth/user.ts";
import { setToken } from "../../models/httpMethods.ts";
import router from "../../router";

export default defineComponent({
    name: "LoginPage",
    props: {
        user: {
            type: String,
            default: ""
        }
    },
    emits: {
        updateUsernameEvent(value: string) {
            return value;
        }
    },
    data: function () {
        return {
            loginData: new LoginData()
        };
    },
    methods: {
        loginUser: async function (): Promise<void> {
            let user: IUser = new User(this.loginData.username, this.loginData.passwd);
            let response = await user.loginUserRequest();
            if (response.status == 200) {
                this.$emit("updateUsernameEvent", response.body.userName);
                setToken(response.body.token);
                await router.push("/");
            } else {
                this.loginData.errors = response.body.errors;
            }
        }
    }
});
</script>
