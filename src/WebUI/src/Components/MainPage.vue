<template>
    <main>
        <div v-if="!user">
            <h1>BikeService</h1>
            <p>Welcome to BikeService</p>
        </div>
        <div v-if="user">
            <h1>BikeService</h1>
            <p>Welcome {{ user }}</p>
        </div>
    </main>
</template>
<script lang="ts">
import { defineComponent, PropType } from "vue";

export default defineComponent({
    name: "MainPage",
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
