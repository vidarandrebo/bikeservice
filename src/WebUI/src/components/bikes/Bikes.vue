<template>
    <new-bike-form @updateBikesEvent="updateBikesHandler"
                   v-bind:equipment-types="equipmentTypes">
    </new-bike-form>
    <bike-view v-for="bike in bikes" :key="bike.id"
               v-bind:bike="bike"
               v-bind:equipment-types="equipmentTypes"
               @updateBikesEvent="updateBikesHandler">
    </bike-view>
</template>
<script lang="ts">

import {defineComponent} from "vue";
import BikeView from "./BikeView.vue";
import NewBikeForm from "./NewBikeForm.vue";
import {Bike, getBikesRequest} from "../../models/bikes/bike.ts";
import {EquipmentType, getTypeRequest} from "../../models/equipmentTypes/equipmentType.ts";

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
        updateUsernameEvent() {
            return true
        },
    },
    data: function () {
        return {
            bikes: [] as Array<Bike>,
            equipmentTypes: [] as Array<EquipmentType>,
        }
    },
    created: async function () {
        const bikesPromise = getBikesRequest();
        const equipmentTypesPromise = getTypeRequest();
        this.bikes = await bikesPromise;
        this.equipmentTypes = await equipmentTypesPromise;
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
