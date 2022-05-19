<template>
    <div class="authentication">
        <form id="register" method="POST" v-on:submit.prevent="registerUser">
            <p v-for="err in registerData.error">{{ err }}</p>
            <label for="username">Username</label>
            <input type="text" id="username" name="username" v-model="registerData.username" required>
            <label for="passwd">Password</label>
            <input type="password" id="passwd" name="passwd" v-model="registerData.passwd" required>
            <label for="repasswd">Repeat password</label>
            <input type="password" id="repasswd" name="repasswd" v-model="registerData.repasswd" required>
            <input type="submit" value="Register">
        </form>
    </div>
</template>
<script lang="ts">

import {defineComponent} from 'vue';
import {IUser, User} from '@/models/auth/user';
import {IRegisterData, RegisterData} from "@/models/auth/register";

export default defineComponent({
    name: 'Register',
    emits: ['updateUsername'],
    props: ["user"],
    data: function () {
        return {
            registerData: new RegisterData(),
        }
    },
    methods: {
        registerUser: async function () {
            this.registerData.passwordRequirementsCheck();
            if (this.registerData.error.length > 0) {
                return
            }
            let user: IUser = new User(
                this.registerData.username,
                this.registerData.passwd
            );
            let response = await user.registerUserRequest();
            if (response.status == 201) {
                this.registerData = new RegisterData();
                await this.$router.push('/login');
            } else {
                this.registerData.error = response.body.errors;
            }
        },
    }
})
</script>
