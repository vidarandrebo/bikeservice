<template>
    <main>
        <article class="flex flex-row justify-between min-w-full">
            <SettingsMenu></SettingsMenu>
            <RouterView class="flex flex-row grow"></RouterView>
        </article>
    </main>
</template>
<script setup lang="ts">
import SettingsMenu from "./SettingsMenu.vue";
</script>
