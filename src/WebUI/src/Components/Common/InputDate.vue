<template>
    <input
        :value="modelValue"
        class="shadow appearance-none border hover:border-gray-500 rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
        type="date"
        @input="handleInput"
    />
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
    name: "InputDate",
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
