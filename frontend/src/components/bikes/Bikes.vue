<template>
    <new-bike-form @updateBikesEvent="updateBikesHandler" v-bind:bike-types="types"></new-bike-form>
    <bike-view v-for="bike in bikes" v-bind:bike="bike" v-bind:bike-types="types" @updateBikesEvent="updateBikesHandler"></bike-view>
</template>
<script lang="ts">
import {defineComponent, PropType} from 'vue';
import NewBikeForm from "@/components/bikes/NewBikeForm.vue";
import {Bike, createBike, IBike} from "@/models/bikes/bike";
import {DataArrayResponse, httpGetWithBody} from "@/models/httpMethods";
import BikeView from "@/components/bikes/BikeView.vue";
import {getTypeRequest, IEquipmentType} from "@/models/equipmentTypes/equipmentType";

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
            types: [] as Array<IEquipmentType>,
        }
    },
    created: async function () {
        await this.getBikes();
        await this.getTypes();
    },
    methods: {
        getBikes: async function () {
            let result = await httpGetWithBody<DataArrayResponse<Bike>>("/api/bike");
            if (result.status === 200) {
                this.bikes = result.body.data.map(createBike);
            }
        },
        getTypes: async function () {
            this.types = await getTypeRequest();
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
