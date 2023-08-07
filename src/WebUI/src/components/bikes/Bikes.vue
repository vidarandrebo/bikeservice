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
import {defineComponent, PropType} from 'vue';
import NewBikeForm from "@/components/bikes/NewBikeForm.vue";
import {Bike, getBikesRequest} from "@/models/bikes/bike";
import BikeView from "@/components/bikes/BikeView.vue";
import {EquipmentType, getTypeRequest} from "@/models/equipmentTypes/equipmentType";

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
