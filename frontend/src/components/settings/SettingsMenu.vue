<template>
    <div class="settings-buttons">
        <router-link to="/settings/account">Account</router-link>
        <router-link to="/settings/bike-types">Bike Types</router-link>
        <router-link to="/settings/part-types">Part Types</router-link>
        <router-link to="#">Something</router-link>
        <router-link to="#">Else</router-link>
    </div>
</template>

<script lang="ts">
import {defineComponent} from "vue";

export default defineComponent({
    name: "SettingsMenu"
})
</script>

<style scoped>
    .settings-buttons {
        background-color: beige;
        margin: 0.5rem 0.25rem 0.5rem 0.5rem;
        padding-left: 1rem;
        padding-right: 1rem;
    }
    .settings-buttons a{
        margin-top: 0.5rem;
        margin-bottom: 0.5rem;
        display: block;
        min-width: 100%;
        width: inherit;
    }

    .router-link-active {
        background-color: burlywood;
    }
</style>
