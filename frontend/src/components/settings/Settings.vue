<template>
    <div class="settings-page">
        <settings-menu></settings-menu>
        <router-view v-bind:user="user"></router-view>
    </div>
</template>
<script lang="ts">
import {defineComponent, PropType} from "vue";
import AccountSettings from "@/components/settings/AccountSettings.vue";
import SettingsMenu from "@/components/settings/SettingsMenu.vue";

export default defineComponent({
    name: 'Settings',
    components: {SettingsMenu, AccountSettings},
    props: {
        user: {
            type: String as PropType<string>,
        }
    },
    emits: {
        updateUsernameEvent(value: string) {
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