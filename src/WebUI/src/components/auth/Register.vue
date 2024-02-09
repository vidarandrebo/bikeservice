<template>
    <div class="card">
        <form id="register" method="POST" @submit.prevent="registerUser">
            <p v-for="(err, i) in registerData.error" :key="i">{{ err }}</p>
            <label for="username">Username</label>
            <input id="username" v-model="registerData.username" type="text" required />
            <label for="passwd">Password</label>
            <input id="passwd" v-model="registerData.passwd" type="password" required />
            <label for="repeat-passwd">Repeat password</label>
            <input id="repeat-passwd" v-model="registerData.repasswd" type="password" required />
            <input type="submit" value="Register" />
        </form>
    </div>
</template>
<script lang="ts">
import { RegisterData } from "../../models/auth/register.ts";
import { defineComponent } from "vue";
import { IUser, User } from "../../models/auth/user.ts";

export default defineComponent({
    name: "RegisterPage",
    props: {
        user: {
            type: String,
            default: ""
        }
    },
    emits: {
        updateUsernameEvent() {
            return true;
        }
    },
    data: function () {
        return {
            registerData: new RegisterData()
        };
    },
    methods: {
        registerUser: async function () {
            this.registerData.passwordRequirementsCheck();
            if (this.registerData.error.length > 0) {
                return;
            }
            let user: IUser = new User(this.registerData.username, this.registerData.passwd);
            let response = await user.registerUserRequest();
            if (response.status == 201) {
                this.registerData = new RegisterData();
                await this.$router.push("/login");
            } else {
                this.registerData.error = response.body.errors;
            }
        }
    }
});
</script>
<style scoped>
div {
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
