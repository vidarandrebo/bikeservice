<template>
    <div class="settings" v-if="user">
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
    <div class="content" v-if="!user">
        <p>Welcome to the settings page! Since you are not logged in, you have no settings to change!</p>
    </div>
</template>
<script lang="ts">
import {AccountSettings} from "@/models/settings/accountSettings";
import {defineComponent, PropType} from "vue";

export default defineComponent({
    name: 'Settings',
    props: {
        user: {
            type: String as PropType<string>,
        }
    },
    emits: {
        updateUsernameEvent(value: string) {
            return true
        },
    },
    data:
        function () {
            return {
                accountSettings: new AccountSettings(),
            }
        },
    methods: {
        changePasswd: async function () {
            console.log("not implemented")
        },
    }
})
</script>
<style scoped>
.settings {
    margin-top: 10px;
    margin-left: auto;
    margin-right: auto;
    max-width: 460px;
    background-color: #ccc;
    border-radius: 10px;
    padding: 20px;
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