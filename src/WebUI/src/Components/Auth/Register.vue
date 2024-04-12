<template>
    <main>
        <article class="max-w-prose">
            <form id="register" method="POST" @submit.prevent="registerUser">
                <p v-for="(err, i) in registerData.error" :key="i">{{ err }}</p>
                <FormField>
                    <LabelPrimary for="username">Username</LabelPrimary>
                    <InputText id="username" v-model="registerData.username" required />
                </FormField>
                <FormField>
                    <LabelPrimary for="passwd">Password</LabelPrimary>
                    <InputText id="passwd" v-model="registerData.passwd" required type="password" />
                </FormField>
                <FormField>
                    <LabelPrimary for="repeat-passwd">Repeat password</LabelPrimary>
                    <InputText id="repeat-passwd" v-model="registerData.repasswd" required type="password" />
                </FormField>
                <FormField>
                    <ButtonPrimary type="submit">Register</ButtonPrimary>
                </FormField>
            </form>
        </article>
    </main>
</template>
<script lang="ts">
import { RegisterData } from "../../Models/Auth/Register.ts";
import { defineComponent } from "vue";
import { IUser, User } from "../../Models/Auth/User.ts";
import InputText from "../Common/InputText.vue";
import ButtonPrimary from "../Common/ButtonPrimary.vue";
import LabelPrimary from "../Common/LabelPrimary.vue";
import FormField from "../Common/FormField.vue";

export default defineComponent({
    name: "RegisterPage",
    components: { FormField, InputText, LabelPrimary, ButtonPrimary },
    props: {
        user: {
            type: String,
            default: ""
        }
    },
    emits: {
        updateUsernameEvent() {
            return true;
        }
    },
    data: function () {
        return {
            registerData: new RegisterData()
        };
    },
    methods: {
        registerUser: async function () {
            this.registerData.passwordRequirementsCheck();
            if (this.registerData.error.length > 0) {
                return;
            }
            let user: IUser = new User(this.registerData.username, this.registerData.passwd);
            let response = await user.registerUserRequest();
            if (response.status == 201) {
                this.registerData = new RegisterData();
                await this.$router.push("/login");
            } else {
                this.registerData.error = response.body.errors;
            }
        }
    }
});
</script>
