<template>
    <header>
        <menubar v-bind:user="user"></menubar>
    </header>
    <main>
        <!--Changes depending on which component is active-->
        <router-view v-bind:user="user"></router-view>
    </main>
    <div class="space"></div>
    <footer>
        <a href="https://github.com/vidarandrebo">https://github.com/vidarandrebo</a>
    </footer>
</template>

<script lang="ts">
import Menubar from './components/menu.vue';
import MainSite from './components/mainsite.vue';
import Bike from './components/bike.vue';
import { defineComponent } from 'vue';
export default defineComponent({
  name: 'App',
  components: {
    Menubar,
    MainSite,
  },
  data: function() {
    return {
        user: null,
        }
    },
})
</script>
