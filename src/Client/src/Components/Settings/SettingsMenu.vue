<template>
    <BulmaMenu>
        <BulmaMenuList>
            <SettingsMenuRouterLink to="/settings/account">
                <p class="p-1">Account</p>
            </SettingsMenuRouterLink>
            <SettingsMenuRouterLink to="/settings/bike-types">
                <p class="p-1">Bike Types</p>
            </SettingsMenuRouterLink>
            <SettingsMenuRouterLink to="/settings/part-types">
                <p class="p-1">Part Types</p>
            </SettingsMenuRouterLink>
        </BulmaMenuList>
    </BulmaMenu>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import SettingsMenuRouterLink from "../Common/SettingsMenuRouterLink.vue";
import BulmaMenu from "../Bulma/Menu/BulmaMenu.vue";
import BulmaMenuList from "../Bulma/Menu/BulmaMenuList.vue";
import BulmaMenuLabel from "../Bulma/Menu/BulmaMenuLabel.vue";

export default defineComponent({
    name: "SettingsMenu",
    components: { BulmaMenuLabel, BulmaMenuList, BulmaMenu, SettingsMenuRouterLink }
});
</script>
