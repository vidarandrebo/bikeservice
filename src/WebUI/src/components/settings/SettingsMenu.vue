<template>
    <div>
        <RouterLink to="/settings/account">Account</RouterLink>
        <RouterLink to="/settings/bike-types">Bike Types</RouterLink>
        <RouterLink to="/settings/part-types">Part Types</RouterLink>
        <RouterLink to="#">Something</RouterLink>
        <RouterLink to="#">Else</RouterLink>
    </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
    name: "SettingsMenu"
});
</script>
