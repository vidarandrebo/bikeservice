<template>
    <button
        class="bg-transparent hover:bg-blue-500 text-blue-700 font-bold hover:text-white py-2 px-4 border border-blue-500 hover:border-transparent rounded"
    >
        <slot></slot>
    </button>
</template>
<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
    name: "ButtonSecondary"
    //class="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline">
});
</script>
