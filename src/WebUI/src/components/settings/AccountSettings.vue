<template>
    <div>
        <form id="changePasswd" method="POST" @submit.prevent="changePasswd">
            <p v-for="(err, i) in accountSettings.errors" :key="i">{{ err }}</p>
            <LabelPrimary for="oldpasswd">Old Password</LabelPrimary>
            <InputText id="oldpasswd" v-model="accountSettings.oldPassword" type="password" name="oldpasswd" required />
            <LabelPrimary for="newpasswd">New Password</LabelPrimary>
            <InputText id="newpasswd" v-model="accountSettings.newPassword" type="password" name="newpasswd" required />
            <LabelPrimary for="renewpasswd">Repeat New Password</LabelPrimary>
            <InputText
                id="renewpasswd"
                v-model="accountSettings.reNewPassword"
                type="password"
                name="renewpasswd"
                required
            />
            <ButtonPrimary type="submit">Change Password"</ButtonPrimary>
        </form>
    </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { AccountSettings } from "../../models/settings/accountSettings.ts";
import LabelPrimary from "../common/LabelPrimary.vue";
import InputText from "../common/InputText.vue";
import ButtonPrimary from "../common/ButtonPrimary.vue";

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
