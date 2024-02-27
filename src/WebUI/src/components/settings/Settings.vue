<template>
    <div>
        <settings-menu></settings-menu>
        <router-view :user="user"></router-view>
    </div>
</template>
<script lang="ts">
import { defineComponent, PropType } from "vue";
import SettingsMenu from "./SettingsMenu.vue";

export default defineComponent({
    name: "SettingsPage",
    components: { SettingsMenu },
    props: {
        user: {
            type: String as PropType<string>,
            default: ""
        }
    },
    emits: {
        updateUsernameEvent() {
            return true;
        }
    }
});
</script>
