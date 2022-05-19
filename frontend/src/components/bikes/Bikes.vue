<template>
    <new-bike-form @updateBikesEvent="updateBikesHandler"></new-bike-form>
    <bike-view v-for="bike in bikes" v-bind:bike="bike" @updateBikesEvent="updateBikesHandler"></bike-view>
</template>
<script lang="ts">
import {defineComponent, PropType} from 'vue';
import NewBikeForm from "@/components/bikes/NewBikeForm.vue";
import {Bike, BikeResponse, IBike} from "@/models/bikes/bike";
import {httpGetWithBody} from "@/models/httpMethods";
import BikeView from "@/components/bikes/BikeView.vue";

export default defineComponent({
    name: 'Bikes',
    emits: ['updateUsername'],
    props: {
        user: {
            type: String as PropType<string>,
        }
    },
    components: {
        BikeView,
        NewBikeForm,
    },
    data: function () {
        return {
            bikes: [] as Array<IBike>,
        }
    },
    created: async function () {
        await this.getBikes();
    },
    methods: {
        getBikes: async function () {
            let result = await httpGetWithBody<BikeResponse>("/bike");
            if (result.status == 200) {
                this.bikes = new Array<IBike>();
                for (let i = 0; i < result.body.bikes.length; i++) {
                    this.bikes.push(new Bike(result.body.bikes[i]));
                }
            }
        },
        updateBikesHandler: async function () {
            await this.getBikes();
        }
    }
})
</script>
