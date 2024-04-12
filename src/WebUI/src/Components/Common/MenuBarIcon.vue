<template>
    <div class="hover:bg-blue-700 p-1 rounded">
        <FontAwesomeIcon :icon="icon" size="2x" v-bind="$attrs"></FontAwesomeIcon>
    </div>
</template>
<script lang="ts">
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
import { defineComponent } from "vue";

export default defineComponent({
    name: "MenuBarIcon",
    components: {
        FontAwesomeIcon
    },
    inheritAttrs: false,
    props: {
        icon: {
            required: true,
            type: String
        }
    }
});
</script>
