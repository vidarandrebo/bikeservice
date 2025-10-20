<template>
    <RouterLink class="navbar-item has-text-black-bold" :to="to">
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
    @apply has-background-primary;
}
</style>
