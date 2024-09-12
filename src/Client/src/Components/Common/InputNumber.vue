<template>
    <input
        :value="modelValue"
        class="shadow appearance-none border hover:border-gray-500 rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
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
