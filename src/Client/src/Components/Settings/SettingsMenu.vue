<template>
    <BMenu>
        <BMenuList>
            <BMenuItem>
                <template #label>
                    <RouterLink to="/settings/account">Account</RouterLink>
                </template>
            </BMenuItem>
            <BMenuItem>
                <template #label>
                    <RouterLink to="/settings/bike-types">Bike Types</RouterLink>
                </template>
            </BMenuItem>
            <BMenuItem>
                <template #label>
                    <RouterLink to="/settings/part-types">Part Types</RouterLink>
                </template>
            </BMenuItem>
        </BMenuList>
    </BMenu>
</template>

<script lang="ts" setup>
import { BMenu, BMenuItem, BMenuList } from "buefy";
</script>
