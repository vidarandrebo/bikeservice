<template>
    <article>
        <div class="flex flex-col items-center grow">
            <div>
                <HeadingH2>Change Password</HeadingH2>
                <form id="changePasswd" method="POST" @submit.prevent="changePasswd">
                    <p v-for="(err, i) in accountSettings.errors" :key="i">{{ err }}</p>
                    <FormField>
                        <LabelPrimary for="oldpasswd">Old Password</LabelPrimary>
                        <InputText
                            id="oldpasswd"
                            v-model="accountSettings.oldPassword"
                            name="oldpasswd"
                            required
                            type="password"
                        />
                    </FormField>
                    <FormField>
                        <LabelPrimary for="newpasswd">New Password</LabelPrimary>
                        <InputText
                            id="newpasswd"
                            v-model="accountSettings.newPassword"
                            name="newpasswd"
                            required
                            type="password"
                        />
                    </FormField>
                    <FormField>
                        <LabelPrimary for="renewpasswd">Repeat New Password</LabelPrimary>
                        <InputText
                            id="renewpasswd"
                            v-model="accountSettings.reNewPassword"
                            name="renewpasswd"
                            required
                            type="password"
                        />
                    </FormField>
                    <FormField>
                        <ButtonPrimary type="submit">Change Password</ButtonPrimary>
                    </FormField>
                </form>
            </div>
        </div>
    </article>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { AccountSettings } from "../../Models/Settings/AccountSettings.ts";
import LabelPrimary from "../Common/LabelPrimary.vue";
import InputText from "../Common/InputText.vue";
import ButtonPrimary from "../Common/ButtonPrimary.vue";
import HeadingH2 from "../Common/Headings/HeadingH2.vue";
import FormField from "../Common/FormField.vue";

export default defineComponent({
    name: "AccountSettings",
    components: { FormField, HeadingH2, ButtonPrimary, InputText, LabelPrimary },
    data: function () {
        return {
            accountSettings: new AccountSettings()
        };
    },
    methods: {
        changePasswd: async function () {
            console.log("not implemented");
        }
    }
});
</script>
