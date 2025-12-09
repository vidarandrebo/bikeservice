<template>
    <input
        :value="modelValue"
        class="input"
        type="number"
        @input="handleInput"
    />
</template>

<script setup lang="ts">
defineProps<{
    modelValue: number;
}>();
const emit = defineEmits(["update:modelValue"]);

function handleInput(event: Event) {
    const inputValue = (event.target as HTMLInputElement).value;
    emit("update:modelValue", inputValue);
}
</script>
