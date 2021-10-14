<template>
    <div class="logregdiv">
        <form id="register" method="POST" v-on:submit="registerUser">
            <p v-if="error">{{ error }}</p>
            <label for="username">Username</label>
            <input type="text" id="username" name="username" v-model="username" required>
            <label for="passwd">Password</label>
            <input type="password" id="passwd" name="passwd" v-model="passwd" required>
            <label for="repasswd">Repeat password</label>
            <input type="password" id="repasswd" name="repasswd" v-model="repasswd" required>
            <input type="submit" value="Register">
        </form>
    </div>
</template>
<script lang="ts">
import { defineComponent } from 'vue';
export default defineComponent({
    name: 'Register',
    data: function() {
        return {
            username: null,
            passwd: null,
            repasswd: null,
            error : null
        }
    },
    methods: {
        registerUser: async function() {
            event.preventDefault();
            this.error = null;
            if (this.passwd !== this.repasswd) {
                this.error = "Passwords do not match";
                return;
            }
            if (this.passwd.length < 8) {
                this.error = "Password should be 8 characters or longer!";
                return;
            }
            let user = {
                "username": this.username,
                "password": this.passwd,
            };
            let response = await fetch('/Register', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(user)
            });
            console.log(response);
            if (response.status == 201) {
                this.username = this.passwd = this.repasswd = null;
                this.$router.push('/login');
            }
        },
    }
})
</script>
