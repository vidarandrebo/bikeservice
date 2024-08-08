<template>
    <main>
        <div v-if="!user.email">
            <h1>BikeService</h1>
            <p>Welcome to BikeService</p>
        </div>
        <div v-if="user.email">
            <h1>BikeService</h1>
            <p>Welcome {{ user.email }}</p>
        </div>
    </main>
</template>
<script setup lang="ts">
import { DefaultUserDependency } from "../Models/Auth/User.ts";
import { inject } from "vue";

const { user } = inject("user", DefaultUserDependency, true);
</script>
