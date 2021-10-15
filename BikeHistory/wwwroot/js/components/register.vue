<template>
    <div class="logregdiv">
        <form id="register" method="POST" v-on:submit="registerUser">
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

import * as Reg from '../register/register';
import { defineComponent } from 'vue';
export default defineComponent({
    name: 'Register',
    data: function() {
        return {
            registerData : new Reg.RegisterData() as Reg.IRegisterData,
        }
    },
    methods: {
        registerUser: async function() {
            event.preventDefault();
            this.registerData.passwordRequirementsCheck();
            if (this.registerData.error.length > 0) {
              return
            }
            let user : Reg.IUser = new Reg.User();
            user.userName = this.registerData.username;
            user.password = this.registerData.passwd;

            let status = await user.registerUserRequest();
            console.log(status);
            if (status === 201) {
                this.registerData = new Reg.RegisterData();
                await this.$router.push('/login');
            }
        },
    }
})
</script>
