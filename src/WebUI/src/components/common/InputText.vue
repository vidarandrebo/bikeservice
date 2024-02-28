<template>
    <input
        class="shadow appearance-none border hover:border-gray-500 rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
        type="text"
        :value="modelValue"
        @input="handleInput"
    />
</template>

<script lang="ts">
import { defineComponent } from "vue";
export default defineComponent({
    name: "InputText",
    props: {
        modelValue: {
            type: String,
            required: true
        }
    },
    emits: ["update:modelValue"],
    methods: {
        handleInput(event: Event) {
            const inputValue = (event.target as HTMLInputElement).value;
            this.$emit("update:modelValue", inputValue);
        }
    }
});
</script>
