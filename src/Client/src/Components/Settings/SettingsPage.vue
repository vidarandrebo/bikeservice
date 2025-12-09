<template>
    <main>
        <div class="columns">
            <aside class="column is-one-fifth">
                <SettingsMenu></SettingsMenu>
            </aside>
            <div class="column">
                <div class="container">
                    <HeadingH1>Settings</HeadingH1>
                    <RouterView class="flex flex-row grow"></RouterView>
                </div>
            </div>
        </div>
    </main>
</template>
<script setup lang="ts">
import SettingsMenu from "./SettingsMenu.vue";
import HeadingH1 from "../Common/Headings/HeadingH1.vue";
</script>
