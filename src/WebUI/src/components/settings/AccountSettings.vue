<template>
    <div class="settings card">
        <form id="changePasswd" method="POST" @submit.prevent="changePasswd">
            <p v-for="(err, i) in accountSettings.errors" :key="i">{{ err }}</p>
            <label for="oldpasswd">Old Password</label>
            <input id="oldpasswd" v-model="accountSettings.oldPassword" type="password" name="oldpasswd" required />
            <label for="newpasswd">New Password</label>
            <input id="newpasswd" v-model="accountSettings.newPassword" type="password" name="newpasswd" required />
            <label for="renewpasswd">Repeat New Password</label>
            <input
                id="renewpasswd"
                v-model="accountSettings.reNewPassword"
                type="password"
                name="renewpasswd"
                required
            />
            <input type="submit" value="Change Password" />
        </form>
    </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { AccountSettings } from "../../models/settings/accountSettings.ts";

export default defineComponent({
    name: "AccountSettings",
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

<style scoped>
.settings {
    flex-grow: 1;
}

form {
    max-width: 300px;
    margin-left: auto;
    margin-right: auto;
}

input {
    display: block;
    width: 100%;
    box-sizing: border-box;
    margin-bottom: 10px;
}
</style>
