<template>
    <div class="logregdiv">
        <form id="register" method="POST" v-on:submit="registerUser">
            <p v-if="registerData.error">{{ registerData.error }}</p>
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

interface IRegisterData {
    username: string;
    passwd: string;
    repasswd: string;
    error: string;
}

class RegisterData {
    username: string;
    passwd: string;
    repasswd: string;
    error: string;
}

interface User {
    username: string;
    password: string;
}

import { defineComponent } from 'vue';
export default defineComponent({
    name: 'Register',
    data: function() {
        return {
            registerData : new RegisterData() as IRegisterData
        }
    },
    methods: {
        registerUser: async function() {
            event.preventDefault();
            this.error = null;
            if (this.registerData.passwd !== this.registerData.repasswd) {
                this.error = "Passwords do not match";
                return;
            }
            if (this.registerData.passwd.length < 8) {
                this.registerData.error = "Password should be 8 characters or longer!";
                return;
            }
            let user : User = {
                username : this.registerData.username,
                password : this.registerData.passwd
            }

            let response = await fetch('/Register', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(user)
            });
            console.log(response);
            if (response.status == 201) {
                this.registerData = new RegisterData();
                this.$router.push('/login');
            }
        },
    }
})
</script>
