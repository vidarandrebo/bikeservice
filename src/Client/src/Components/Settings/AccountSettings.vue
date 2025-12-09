<template>
    <article>
        <div class="flex flex-col items-center grow">
            <div>
                <HeadingH2>Change Password</HeadingH2>
                <form id="changePasswd" method="POST" @submit.prevent="changePassword">
                    <p v-for="(err, i) in accountSettings.errors" :key="i">{{ err }}</p>
                    <BField label="Old Password">
                        <InputText
                            id="oldpasswd"
                            v-model="accountSettings.oldPassword"
                            name="oldpasswd"
                            required
                            type="password"
                        />
                    </BField>
                    <BField label="New Password">
                        <InputText
                            id="newpasswd"
                            v-model="accountSettings.newPassword"
                            name="newpasswd"
                            required
                            type="password"
                        />
                    </BField>
                    <BField label="Repeat New Password">
                        <InputText
                            id="renewpasswd"
                            v-model="accountSettings.reNewPassword"
                            name="renewpasswd"
                            required
                            type="password"
                        />
                    </BField>
                    <BField>
                        <BButton type="is-primary">Change Password</BButton>
                    </BField>
                </form>
            </div>
        </div>
    </article>
</template>

<script setup lang="ts">
import { ref } from "vue";
import { AccountSettings } from "../../Models/Settings/AccountSettings.ts";
import InputText from "../Common/InputText.vue";
import HeadingH2 from "../Common/Headings/HeadingH2.vue";
import { BButton, BField } from "buefy";

const accountSettings = ref(new AccountSettings());

async function changePassword() {
    console.log("not implemented");
}
</script>
