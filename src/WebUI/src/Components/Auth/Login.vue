<template>
    <main>
        <article class="max-w-prose">
            <form id="login" method="POST" @submit.prevent="loginUser">
                <p v-for="(err, i) in loginData.errors" :key="i">{{ err }}</p>
                <FormField>
                    <LabelPrimary for="username">Username</LabelPrimary>
                    <InputText id="username" v-model="loginData.username" name="username" required />
                </FormField>
                <FormField>
                    <LabelPrimary for="passwd">Password</LabelPrimary>
                    <InputText id="passwd" v-model="loginData.passwd" name="passwd" required type="password" />
                </FormField>
                <FormField>
                    <ButtonPrimary type="submit">Login</ButtonPrimary>
                </FormField>
            </form>
        </article>
    </main>
</template>
<script lang="ts">
import { defineComponent } from "vue";
import { LoginData } from "../../Models/Auth/Login.ts";
import { IUser, User } from "../../Models/Auth/User.ts";
import { setToken } from "../../Models/HttpMethods.ts";
import router from "../../Router";
import ButtonPrimary from "../Common/ButtonPrimary.vue";
import LabelPrimary from "../Common/LabelPrimary.vue";
import InputText from "../Common/InputText.vue";
import FormField from "../Common/FormField.vue";

export default defineComponent({
    name: "LoginPage",
    components: { FormField, InputText, LabelPrimary, ButtonPrimary },
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
