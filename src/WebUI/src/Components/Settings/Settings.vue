<template>
    <main>
        <article class="flex flex-row justify-between min-w-full">
            <SettingsMenu></SettingsMenu>
            <RouterView :user="user" class="flex flex-row grow"></RouterView>
        </article>
    </main>
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
