<template>
    <li>
        <RouterLink :to="to" activeClass="is-active">
            <slot></slot>
        </RouterLink>
    </li>
</template>

<script setup lang="ts">
defineProps<{
    to: string;
}>();
</script>