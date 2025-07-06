<template>
    <main>
        <article class="max-w-prose">
            <form id="register" method="POST" @submit.prevent="registerUser">
                <p v-for="(err, i) in registerData.errors" :key="i">{{ err }}</p>
                <FormField>
                    <LabelPrimary for="email">Email</LabelPrimary>
                    <InputText id="email" v-model="registerData.email" required />
                </FormField>
                <FormField>
                    <LabelPrimary for="password">Password</LabelPrimary>
                    <InputText id="password" v-model="registerData.password" required type="password" />
                </FormField>
                <FormField>
                    <LabelPrimary for="repeat-password">Repeat password</LabelPrimary>
                    <InputText id="repeat-password" v-model="registerData.repeatPassword" required type="password" />
                </FormField>
                <FormField>
                    <ButtonPrimary type="submit">Register</ButtonPrimary>
                </FormField>
            </form>
            <template v-for="(errorCategory, key) in errs?.errors" :key="key">
                <p v-for="(a, b) in errorCategory" :key="b">{{ a.replace("Error with Message=", "") }}</p>
            </template>
        </article>
    </main>
</template>
<script setup lang="ts">
import { ref } from "vue";
import { Credentials } from "../../Models/Auth/Credentials.ts";
import InputText from "../Common/InputText.vue";
import ButtonPrimary from "../Common/ButtonPrimary.vue";
import LabelPrimary from "../Common/LabelPrimary.vue";
import FormField from "../Common/FormField.vue";
import router from "../../Router";
import { HttpValidationProblemDetails } from "../../Gen";

const registerData = ref<Credentials>(new Credentials());
const errs = ref<HttpValidationProblemDetails | null>(null);

async function registerUser() {
    registerData.value.passwordRequirementsCheck();
    if (registerData.value.errors.length > 0) {
        return;
    }
    const httpValidationProblemDetails = await registerData.value.registerUserRequest();
    if (!httpValidationProblemDetails) {
        registerData.value = new Credentials();
        await router.push("/login");
    } else {
        errs.value = httpValidationProblemDetails;
    }
}
</script>
