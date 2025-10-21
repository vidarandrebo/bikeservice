<template>
    <main>
        <article class="max-w-prose">
            <form id="register" method="POST" @submit.prevent="registerUser">
                <p v-for="(err, i) in registerData.errors" :key="i">{{ err }}</p>
                <BField label="Email">
                    <InputText id="email" v-model="registerData.email" required />
                </BField>
                <BField label="Password">
                    <InputText id="password" v-model="registerData.password" required type="password" />
                </BField>
                <BField label="Repeat password">
                    <InputText id="repeat-password" v-model="registerData.repeatPassword" required type="password" />
                </BField>
                <BField>
                    <BButton type="is-primary">Register</BButton>
                </BField>
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
import router from "../../Router";
import { HttpValidationProblemDetails } from "../../Gen";
import { BButton, BField } from "buefy";

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
