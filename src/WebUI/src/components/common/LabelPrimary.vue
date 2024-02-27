<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
    name: "LabelPrimary"
});
</script>

<template>
    <label class="block text-gray-700 text-sm font-bold">
        <slot></slot>
    </label>
</template>

<style scoped></style>
