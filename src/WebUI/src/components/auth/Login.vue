<template>
    <div>
        <form id="login" method="POST" @submit.prevent="loginUser">
            <p v-for="(err, i) in loginData.errors" :key="i">{{ err }}</p>
            <LabelPrimary for="username">Username</LabelPrimary>
            <InputText id="username" v-model="loginData.username" name="username" required />
            <LabelPrimary for="passwd">Password</LabelPrimary>
            <InputText id="passwd" v-model="loginData.passwd" type="password" name="passwd" required />
            <ButtonPrimary type="submit">Login</ButtonPrimary>
        </form>
    </div>
</template>
<script lang="ts">
import { defineComponent } from "vue";
import { LoginData } from "../../models/auth/login.ts";
import { IUser, User } from "../../models/auth/user.ts";
import { setToken } from "../../models/httpMethods.ts";
import router from "../../router";
import ButtonPrimary from "../common/ButtonPrimary.vue";
import LabelPrimary from "../common/LabelPrimary.vue";
import InputText from "../common/InputText.vue";

export default defineComponent({
    name: "LoginPage",
    components: { InputText, LabelPrimary, ButtonPrimary },
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
