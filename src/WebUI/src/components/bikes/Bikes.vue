<template>
    <new-bike-form @updateBikesEvent="updateBikesHandler" v-bind:equipment-types="equipmentTypes"></new-bike-form>
    <bike-view v-for="bike in bikes" :key="bike.id" v-bind:bike="bike" v-bind:equipment-types="equipmentTypes"
               @updateBikesEvent="updateBikesHandler"></bike-view>
</template>
<script lang="ts">
import {defineComponent} from 'vue';
import NewBikeForm from "@/components/bikes/NewBikeForm.vue";
import {getBikesRequest, IBike} from "@/models/bikes/bike";
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
            type: String
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
            equipmentTypes: [] as Array<IEquipmentType>,
        }
    },
    created: async function () {
        this.bikes = await getBikesRequest();
        this.equipmentTypes = await getTypeRequest();
    },
    methods: {
        /**
         * Handler for updateBikesEvent
         */

        updateBikesHandler: async function () {
            this.bikes = await getBikesRequest();
        }
    }
})
</script>
