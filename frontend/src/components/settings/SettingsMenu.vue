<template>
    <div class="settings-buttons">
        <button>Account</button>
        <button>Equipment types</button>
        <button>Bike types</button>
        <button>Account</button>
        <button>Equipment types</button>
        <button>Bike types</button>
        <button>Account</button>
        <button>Equipment types</button>
        <button>Bike types</button>
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
    .settings-buttons button{
        margin-top: 0.5rem;
        margin-bottom: 0.5rem;
        display: block;
        min-width: 100%;
        width: inherit;
    }
</style>