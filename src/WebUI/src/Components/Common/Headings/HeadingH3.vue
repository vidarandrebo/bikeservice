<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
    name: "HeadingH3"
});
</script>

<template>
    <h3 class="text-2xl font-bold">
        <slot></slot>
    </h3>
</template>

<style scoped></style>
