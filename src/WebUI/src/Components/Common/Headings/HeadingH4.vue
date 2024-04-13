<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
    name: "HeadingH4"
});
</script>

<template>
    <h4 class="text-xl font-bold">
        <slot></slot>
    </h4>
</template>

<style scoped></style>
