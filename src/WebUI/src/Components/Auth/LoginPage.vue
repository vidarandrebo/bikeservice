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
<script setup lang="ts">
import { inject, ref } from "vue";
import { LoginData } from "../../Models/Auth/Login.ts";
import { Credentials, ICredentials } from "../../Models/Auth/Credentials.ts";
import { setToken } from "../../Models/HttpMethods.ts";
import router from "../../Router";
import ButtonPrimary from "../Common/ButtonPrimary.vue";
import LabelPrimary from "../Common/LabelPrimary.vue";
import InputText from "../Common/InputText.vue";
import FormField from "../Common/FormField.vue";
import { DefaultUserDependency, User } from "../../Models/Auth/User.ts";
import { DefaultBikeCollection } from "../../Models/Bikes/BikeCollection.ts";
import { DefaultPartCollection } from "../../Models/Parts/PartCollection.ts";
import { DefaultEquipmentTypeCollection } from "../../Models/EquipmentTypes/EquipmentTypeCollection.ts";

const loginData = ref<LoginData>(new LoginData());

const { setUser } = inject("user", DefaultUserDependency, true);
const { fetchBikes } = inject("bikes", DefaultBikeCollection, true);
const { fetchParts } = inject("parts", DefaultPartCollection, true);
const { fetchEquipmentTypes } = inject("equipmentTypes", DefaultEquipmentTypeCollection, true);

async function loginUser(): Promise<void> {
    let credentials: ICredentials = new Credentials(loginData.value.username, loginData.value.passwd);
    let response = await credentials.loginUserRequest();
    if (response.status == 200) {
        setToken(response.body.token);
        setUser(new User(response.body.userName));
        fetchBikes();
        fetchParts();
        fetchEquipmentTypes();
        await router.push("/");
    } else {
        loginData.value.errors = response.body.errors;
    }
}
</script>
