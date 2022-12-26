<template>
    <div class="content" v-if="!user">
        <p>Welcome to the page! To like posts and follow other users, click the register button in the top right corner.
            Already have an account? Log in instead! Remember that you can view all of the post on the site by clicking
            on the globe, even if you are not logged in.</p>
    </div>
    <div class="content" v-if="user">
        <p>Welcome to the page! It seems you are able to log in</p>
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
        updateUsernameEvent(value: string) {
            return true
        },
    },
})
</script>
