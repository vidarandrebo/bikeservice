<template>
    <Suspense>
        <DependencyInjection>
            <div class="flex flex-col h-screen">
                <MenuBar></MenuBar>
                <RouterView class="grow flex flex-row justify-center"></RouterView>
                <PageFooter></PageFooter>
            </div>
        </DependencyInjection>
    </Suspense>
</template>

<script setup lang="ts">
import MenuBar from "./Components/MenuBar.vue";
import PageFooter from "./Components/PageFooter.vue";
import DependencyInjection from "./DependencyInjection.vue";
</script>
