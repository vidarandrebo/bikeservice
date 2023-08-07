<template>
    <div class="card" v-if="!user">
        <h1>BikeService</h1>
        <p>Welcome to BikeService</p>
    </div>
    <div class="card" v-if="user">
        <h1>BikeService</h1>
        <p>Welcome {{ user }}</p>
    </div>
</template>
<script lang="ts">
import {defineComponent, PropType} from 'vue';

export default defineComponent({
    name: 'MainSite',
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
