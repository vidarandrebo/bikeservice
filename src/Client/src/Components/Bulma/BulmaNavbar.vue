<script setup lang="ts">

import { computed, ref } from "vue";

const slots = defineSlots<{
    start(): void
    end(): void
}>();
const burgerIsOpen = ref<boolean>(false);
const navbarBurgerClass = computed(() => {
    if (burgerIsOpen.value) {
        return "navbar-burger js-burger is-active";
    }
    return "navbar-burger js-burger";
});
const navbarMenuClass = computed(() => {
    if (burgerIsOpen.value) {
        return "navbar-menu is-active";
    }
    return "navbar-menu";
});

function flipBurger() {
    burgerIsOpen.value = !burgerIsOpen.value;
}
</script>

<template>
    <nav class="navbar is-active has-background-primary-soft">
        <div class="navbar-brand">
            <div :class="navbarBurgerClass" data-target="navbarExampleTransparentExample" @click="flipBurger">
                <span></span>
                <span></span>
                <span></span>
                <span></span>
            </div>
        </div>
        <div :class="navbarMenuClass" @click="flipBurger">
            <div class="navbar-start">
                <slot name="start"></slot>
            </div>
            <div class="navbar-end">
                <slot name="end"></slot>
            </div>
        </div>
    </nav>
</template>

<style scoped>

</style>