<template>
    <input
        :value="modelValue"
        class="input"
        type="text"
        @input="handleInput"
    />
</template>

<script setup lang="ts">
defineProps<{
    modelValue: string;
}>();
const emit = defineEmits(["update:modelValue"]);

function handleInput(event: Event) {
    const inputValue = (event.target as HTMLInputElement).value;
    emit("update:modelValue", inputValue);
}
</script>
