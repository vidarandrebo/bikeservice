<template>
    <div class="logregdiv">
        <form id="login" method="POST" v-on:submit.prevent="loginUser">
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
import {defineComponent} from 'vue';

export default defineComponent({
    name: 'Login',
    data: function () {
        return {
            username: null,
            passwd: null,
            error: null
        }
    },
    emits: ['fetchUsername'],
    methods: {
        loginUser: async function (): Promise<void> {
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
            let testLogin = await fetch('/Login', {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': result["token"] + "hei"
                }
            });
            this.$emit('fetchUsername', 'rolf');
            console.log(testLogin);
        },
    }
})
</script>
