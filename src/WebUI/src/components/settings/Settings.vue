<template>
    <div class="settings-page">
        <settings-menu></settings-menu>
        <router-view v-bind:user="user"></router-view>
    </div>
</template>
<script lang="ts">

import {defineComponent, PropType} from "vue";
import SettingsMenu from "./SettingsMenu.vue";

export default defineComponent({
    name: 'Settings',
    components: {SettingsMenu},
    props: {
        user: {
            type: String as PropType<string>,
        }
    },
    emits: {
        updateUsernameEvent() {
            return true
        },
    },
})
</script>
<style scoped>
.settings-page {
    display: flex;
}

</style>
