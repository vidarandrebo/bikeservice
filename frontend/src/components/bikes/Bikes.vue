<template>
    <new-bike-form @updateBikesEvent="updateBikesHandler"></new-bike-form>
    <bike-view v-for="bike in bikes" v-bind:bike="bike" @updateBikesEvent="updateBikesHandler"></bike-view>
</template>
<script lang="ts">
import {defineComponent, PropType} from 'vue';
import NewBikeForm from "@/components/bikes/NewBikeForm.vue";
import {Bike, createBike, IBike} from "@/models/bikes/bike";
import {DataArrayResponse, httpGetWithBody} from "@/models/httpMethods";
import BikeView from "@/components/bikes/BikeView.vue";

export default defineComponent({
    name: 'Bikes',
    components: {
        BikeView,
        NewBikeForm,
    },
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
            let result = await httpGetWithBody<DataArrayResponse<Bike>>("/bike");
            if (result.status === 200) {
                this.bikes = result.body.data.map(createBike);
            }
        },
        /**
         * Handler for updateBikesEvent
         */
        updateBikesHandler: async function () {
            await this.getBikes();
        }
    }
})
</script>
