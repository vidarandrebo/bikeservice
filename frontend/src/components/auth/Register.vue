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

import {defineComponent, PropType} from 'vue';
import {IUser, User} from '@/models/auth/user';
import {RegisterData} from "@/models/auth/register";

export default defineComponent({
    name: 'Register',
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
<style scoped>
.authentication {
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
