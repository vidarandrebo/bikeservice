<template>
    <div class="logregdiv">
        <form id="login" method="POST" v-on:submit="loginUser">
            <p v-if="error">{{ error }}</p>
            <label for="username">Username</label>
            <input type="text" id="username" name="username" v-model="username" required>
            <label for="passwd">Password</label>
            <input type="password" id="passwd" name="passwd" v-model="passwd" required>
            <input type="submit" value="Login">
        </form>
    </div>
</template>
<script lang="ts">
import { defineComponent } from 'vue';
export default defineComponent({
    name: 'Login',
    data: function() {
        return {
            username: null,
            passwd: null,
            error: null
        }
    },
    methods: {
        loginUser: async function() {
            event.preventDefault();
            this.error = null;
            let user = {
                "username": this.username,
                "password": this.passwd
            };
            let response = await fetch('/Login', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(user)
            });
            let result = await response.json();
            console.log(result["token"]);
           //if (result['login'] == "success") {
           //    this.username = this.passwd = null;
           //    this.$root.user = await getUsername();
           //    router.push('/');
           //} else {
           //    this.error = result['error'];
           //}
            let testlogin = await fetch('/Login', {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': result["token"] + "hei"
                }
                
            });
            console.log(testlogin);
        },
    }
})
</script>
