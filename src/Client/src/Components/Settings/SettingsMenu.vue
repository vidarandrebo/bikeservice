<template>
    <div class="flex flex-col">
        <SettingsMenuRouterLink to="/settings/account">
            <p class="p-1">Account</p>
        </SettingsMenuRouterLink>
        <SettingsMenuRouterLink to="/settings/bike-types">
            <p class="p-1">Bike Types</p>
        </SettingsMenuRouterLink>
        <SettingsMenuRouterLink to="/settings/part-types">
            <p class="p-1">Part Types</p>
        </SettingsMenuRouterLink>
    </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import SettingsMenuRouterLink from "../Common/SettingsMenuRouterLink.vue";

export default defineComponent({
    name: "SettingsMenu",
    components: { SettingsMenuRouterLink }
});
</script>
