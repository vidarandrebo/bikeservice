<template>
    <main>
        <section class="section">
            <div class="container">
                <form id="login" method="POST" @submit.prevent="loginUser">
                    <p v-for="(err, i) in loginData.errors" :key="i">{{ err }}</p>
                    <BField label="Email">
                        <BInput v-model="loginData.email" type="email" required></BInput>
                    </BField>
                    <BField label="Password">
                        <BInput id="passwd" v-model="loginData.password" name="passwd" required type="password" />
                    </BField>
                    <BField>
                        <BButton type="is-primary" @click="loginUser">Login</BButton>
                    </BField>
                </form>
            </div>
        </section>
    </main>
</template>
<script setup lang="ts">
import { inject, ref } from "vue";
import { Credentials } from "../../Models/Auth/Credentials.ts";
import router from "../../Router";
import { DefaultUserDependency } from "../../Models/Auth/User.ts";
import { DefaultBikeCollection } from "../../Models/Bikes/BikeCollection.ts";
import { DefaultPartCollection } from "../../Models/Parts/PartCollection.ts";
import { DefaultEquipmentTypeCollection } from "../../Models/EquipmentTypes/EquipmentTypeCollection.ts";
import { BButton, BField, BInput } from "buefy";

const loginData = ref<Credentials>(new Credentials());

const { setUser } = inject("user", DefaultUserDependency, true);
const { fetchBikes } = inject("bikes", DefaultBikeCollection, true);
const { fetchParts } = inject("parts", DefaultPartCollection, true);
const { fetchEquipmentTypes } = inject("equipmentTypes", DefaultEquipmentTypeCollection, true);

async function loginUser(): Promise<void> {
    const user = await loginData.value.loginUserRequest();
    if (user) {
        setUser(user);
        fetchBikes();
        fetchParts();
        fetchEquipmentTypes();
        await router.push("/");
    } else {
        //loginData.value.errors = response.body.errors;
    }
}
</script>
