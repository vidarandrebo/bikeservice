<template>
    <main>
        <HeadingH1>BikeService</HeadingH1>
        <div v-if="!user.email">
            <p>Welcome to BikeService</p>
        </div>
        <div v-if="user.email">
            <p>Welcome {{ user.email }}</p>
        </div>
    </main>
</template>
<script setup lang="ts">
import { DefaultUserDependency } from "../Models/Auth/User.ts";
import { inject } from "vue";
import HeadingH1 from "./Common/Headings/HeadingH1.vue";

const { user } = inject("user", DefaultUserDependency, true);
</script>
