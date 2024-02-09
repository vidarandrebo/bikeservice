<template>
    <div class="card settings-buttons">
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

<style scoped>
.settings-buttons a {
    margin-top: 0.5rem;
    margin-bottom: 0.5rem;
    display: block;
    min-width: 100%;
    width: inherit;
}

.router-link-active {
    background-color: var(--highlight-color);
}

a:visited {
    color: var(--text-color);
}
</style>
