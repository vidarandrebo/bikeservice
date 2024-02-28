<template>
    <p class="py-1">
        <slot></slot>
    </p>
</template>
<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
    name: "FormField"
});
</script>