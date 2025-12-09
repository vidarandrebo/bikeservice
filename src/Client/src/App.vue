<template>
    <DependencyInjection>
        <div class="flex flex-col h-screen">
            <MenuBar></MenuBar>
            <RouterView class="grow flex flex-row justify-center"></RouterView>
            <PageFooter></PageFooter>
        </div>
    </DependencyInjection>
</template>

<script setup lang="ts">
import MenuBar from "./Components/MenuBar.vue";
import PageFooter from "./Components/PageFooter.vue";
import DependencyInjection from "./DependencyInjection.vue";
</script>

<style lang="scss">
@use "bulma/sass" with (
    //$text: hsl(20, 100%, 10),
    $link: rgba(0, 112, 112, 1),
    $primary: rgba(0, 112, 112, 1),
    $info: rgba(36, 55, 70, 1),
    $success: rgba(75, 183, 72, 1),
    $warning: rgba(255, 146, 0, 1),
    $danger: rgba(235, 0, 0, 1),
    $family-primary:
        "Inter, SF Pro, Segoe UI, Roboto, Oxygen, Ubuntu, Helvetica Neue, Helvetica, Arial, sans-serif, IBM Plex",
    $family-secondary:
        "Inter, SF Pro, Segoe UI, Roboto, Oxygen, Ubuntu, Helvetica Neue, Helvetica, Arial, sans-serif, IBM Plex",
    $family-code: "Inconsolata, Hack, SF Mono, Roboto Mono, Source Code Pro, Ubuntu Mono, monospace, IBM Plex"
);
table {
    width: 100%;
}
</style>
