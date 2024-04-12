<template>
    <div>
        <form id="changePasswd" method="POST" @submit.prevent="changePasswd">
            <p v-for="(err, i) in accountSettings.errors" :key="i">{{ err }}</p>
            <LabelPrimary for="oldpasswd">Old Password</LabelPrimary>
            <InputText id="oldpasswd" v-model="accountSettings.oldPassword" name="oldpasswd" required type="password" />
            <LabelPrimary for="newpasswd">New Password</LabelPrimary>
            <InputText id="newpasswd" v-model="accountSettings.newPassword" name="newpasswd" required type="password" />
            <LabelPrimary for="renewpasswd">Repeat New Password</LabelPrimary>
            <InputText
                id="renewpasswd"
                v-model="accountSettings.reNewPassword"
                name="renewpasswd"
                required
                type="password"
            />
            <ButtonPrimary type="submit">Change Password"</ButtonPrimary>
        </form>
    </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { AccountSettings } from "../../Models/Settings/AccountSettings.ts";
import LabelPrimary from "../Common/LabelPrimary.vue";
import InputText from "../Common/InputText.vue";
import ButtonPrimary from "../Common/ButtonPrimary.vue";

export default defineComponent({
    name: "AccountSettings",
    components: { ButtonPrimary, InputText, LabelPrimary },
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
