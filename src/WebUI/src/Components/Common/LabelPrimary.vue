<template>
    <label class="block text-sm font-bold">
        <slot></slot>
    </label>
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
    name: "LabelPrimary"
});
</script>
