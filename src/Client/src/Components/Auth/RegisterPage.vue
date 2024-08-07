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
<script setup lang="ts">
import { RegisterData } from "../../Models/Auth/Register.ts";
import { ref } from "vue";
import { Credentials, ICredentials } from "../../Models/Auth/Credentials.ts";
import InputText from "../Common/InputText.vue";
import ButtonPrimary from "../Common/ButtonPrimary.vue";
import LabelPrimary from "../Common/LabelPrimary.vue";
import FormField from "../Common/FormField.vue";
import router from "../../Router";

const registerData = ref<Credentials>(new Credentials());

async function registerUser() {
    registerData.value.passwordRequirementsCheck();
    if (registerData.value.error.length > 0) {
        return;
    }
    let response = await registerData.registerUserRequest();
    if (response.status == 201) {
        registerData.value = new RegisterData();
        await router.push("/login");
    } else {
        registerData.value.error = response.body.errors;
    }
}
</script>
