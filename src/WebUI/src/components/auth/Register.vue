<template>
    <div>
        <form id="register" method="POST" @submit.prevent="registerUser">
            <p v-for="(err, i) in registerData.error" :key="i">{{ err }}</p>
            <LabelPrimary for="username">Username</LabelPrimary>
            <InputText id="username" v-model="registerData.username" required />
            <LabelPrimary for="passwd">Password</LabelPrimary>
            <InputText id="passwd" v-model="registerData.passwd" type="password" required />
            <LabelPrimary for="repeat-passwd">Repeat password</LabelPrimary>
            <InputText id="repeat-passwd" v-model="registerData.repasswd" type="password" required />
            <ButtonPrimary type="submit">Register</ButtonPrimary>
        </form>
    </div>
</template>
<script lang="ts">
import { RegisterData } from "../../models/auth/register.ts";
import { defineComponent } from "vue";
import { IUser, User } from "../../models/auth/user.ts";
import InputText from "../common/InputText.vue";
import ButtonPrimary from "../common/ButtonPrimary.vue";
import LabelPrimary from "../common/LabelPrimary.vue";

export default defineComponent({
    name: "RegisterPage",
    components: { InputText, LabelPrimary, ButtonPrimary },
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
