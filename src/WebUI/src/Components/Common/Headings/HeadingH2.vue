<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
    name: "HeadingH2"
});
</script>

<template>
    <h2 class="text-3xl font-bold">
        <slot></slot>
    </h2>
</template>

<style scoped></style>
