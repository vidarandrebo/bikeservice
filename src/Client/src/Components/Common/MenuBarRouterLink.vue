<template>
    <RouterLink :to="to">
        <slot></slot>
    </RouterLink>
</template>

<script setup lang="ts">
defineProps<{
    to: string;
}>();
</script>

<style scoped>
.router-link-active {
    @apply bg-blue-400 rounded;
}
</style>
