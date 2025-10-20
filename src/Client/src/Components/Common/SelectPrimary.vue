<template>
    <div class="select">
        <select
            :value="modelValue"
            v-bind="$attrs"
            @input="handleInput"
        >
            <slot></slot>
        </select>
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
