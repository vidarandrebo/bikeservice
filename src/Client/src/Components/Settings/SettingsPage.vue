<template>
    <main>
        <article class="flex flex-row justify-between max-w-screen-lg w-full p-4">
            <div>
                <HeadingH1 class="">Settings</HeadingH1>
                <SettingsMenu></SettingsMenu>
            </div>
            <RouterView class="flex flex-row grow"></RouterView>
        </article>
    </main>
</template>
<script setup lang="ts">
import SettingsMenu from "./SettingsMenu.vue";
import HeadingH1 from "../Common/Headings/HeadingH1.vue";
</script>
