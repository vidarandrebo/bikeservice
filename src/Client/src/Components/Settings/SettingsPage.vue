<template>
    <main>
        <div class="columns">
            <aside class="column is-one-fifth">
                <SettingsMenu></SettingsMenu>
            </aside>
            <div class="column">
                <section class="section">
                    <div class="container">
                        <HeadingH1>Settings</HeadingH1>
                        <RouterView class=""></RouterView>
                    </div>
                </section>
            </div>
        </div>
    </main>
</template>
<script setup lang="ts">
import SettingsMenu from "./SettingsMenu.vue";
import HeadingH1 from "../Common/Headings/HeadingH1.vue";
</script>
