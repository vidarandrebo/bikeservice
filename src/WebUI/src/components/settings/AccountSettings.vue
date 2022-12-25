<template>
    <div class="settings">
        <form id="changePasswd" method="POST" v-on:submit.prevent="changePasswd">
            <p v-for="err in accountSettings.errors">{{ err }}</p>
            <label for="oldpasswd">Old Password</label>
            <input type="password" name="oldpasswd" id="oldpasswd" v-model="accountSettings.oldPassword" required>
            <label for="newpasswd">New Password</label>
            <input type="password" name="newpasswd" id="newpasswd" v-model="accountSettings.newPassword" required>
            <label for="renewpasswd">Repeat New Password</label>
            <input type="password" name="renewpasswd" id="renewpasswd" v-model="accountSettings.reNewPassword" required>
            <input type="submit" value="Change Password">
        </form>
    </div>
</template>

<script lang="ts">
import {AccountSettings} from "@/models/settings/accountSettings";
import {defineComponent, PropType} from "vue";

export default defineComponent({
    name: "AccountSettings",
    data: function () {
        return {
            accountSettings: new AccountSettings(),
        }
    },
    methods: {
        changePasswd: async function () {
            console.log("not implemented")
        },
    },

})
</script>

<style scoped>
.settings {
    background-color: beige;
    margin: 0.5rem 0.5rem 0.5rem 0.25rem;
    padding: 1rem;
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