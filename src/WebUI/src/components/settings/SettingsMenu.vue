<template>
    <div>
        <router-link to="/settings/account">Account</router-link>
        <router-link to="/settings/bike-types">Bike Types</router-link>
        <router-link to="/settings/part-types">Part Types</router-link>
        <router-link to="#">Something</router-link>
        <router-link to="#">Else</router-link>
    </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
    name: "SettingsMenu"
});
</script>
