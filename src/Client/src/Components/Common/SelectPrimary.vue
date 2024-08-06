<template>
    <div class="inline-block relative w-full">
        <select
            :value="modelValue"
            class="block appearance-none w-full bg-white border hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline text-gray-700"
            v-bind="$attrs"
            @input="handleInput"
        >
            <slot></slot>
        </select>
        <div class="pointer-events-none absolute inset-y-0 right-0 flex items-center px-2 text-gray-700">
            <svg class="fill-current h-4 w-4" viewBox="0 0 20 20" xmlns="http://www.w3.org/2000/svg">
                <path d="M9.293 12.95l.707.707L15.657 8l-1.414-1.414L10 10.828 5.757 6.586 4.343 8z" />
            </svg>
        </div>
    </div>
</template>

<script setup lang="ts">
defineOptions({
    inheritAttrs: false
});
defineProps<{
    modelValue: string;
}>();
const emit = defineEmits(["update:modelValue"]);

function handleInput(event: Event) {
    const inputValue = (event.target as HTMLInputElement).value;
    emit("update:modelValue", inputValue);
}
</script>
