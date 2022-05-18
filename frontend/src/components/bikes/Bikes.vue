<template>
    <new-bike-form></new-bike-form>
    <div class="content">
        <bike-view v-for="bike in bikes" v-bind:bike="bike"></bike-view>
    </div>
</template>
<script lang="ts">
import {defineComponent} from 'vue';
import NewBikeForm from "@/components/bikes/NewBikeForm.vue";
import {Bike, BikeResponse} from "@/models/bikes/bike";
import {getWithBody} from "@/models/genericFetch";
import BikeView from "@/components/bikes/BikeView.vue";

export default defineComponent({
    name: 'Bikes',
    emits: ['updateUsername'],
    props: ["user"],
    components: {
        BikeView,
        NewBikeForm,
    },
    data: function () {
        return {
            bikes: [] as Array<Bike>,
        }
    },
    created: async function () {
        await this.getBikes();
    },
    methods: {
        getBikes: async function () {
            let result = await getWithBody<BikeResponse>("/bike");
            if (result.status == 200) {
                this.bikes = result.body.bikes;
            }
        }
    }
})
</script>
