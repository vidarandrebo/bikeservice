<template>
    <div class="hover:bg-blue-700 p-1 rounded ">
        <FontAwesomeIcon :icon="icon" size="2x" v-bind="$attrs"></FontAwesomeIcon>
    </div>
</template>
<script setup lang="ts">
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";

defineOptions({
    inheritAttrs: false
});
defineProps<{
    icon: string;
}>();
</script>

<style>
</style>