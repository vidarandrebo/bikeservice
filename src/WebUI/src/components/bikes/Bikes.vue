<template>
    <main>
        <article class="flex flex-col items-center justify-center">
            <h1 class="min-w-full">Bikes</h1>
            <NewBikeForm :equipment-types="equipmentTypes" @update-bikes-event="updateBikesHandler" class="min-w-full"></NewBikeForm>
            <ol class="flex flex-col space-y-2 min-w-full">
                <li v-for="bike in bikes" :key="bike.id" >
                    <BikeView :bike="bike" :equipment-types="equipmentTypes" @update-bikes-event="updateBikesHandler">
                    </BikeView>
                </li>
            </ol>
        </article>
    </main>
</template>
<script lang="ts">
import { defineComponent } from "vue";
import BikeView from "./BikeView.vue";
import NewBikeForm from "./NewBikeForm.vue";
import { Bike, getBikesRequest } from "../../models/bikes/bike.ts";
import { EquipmentType, getTypeRequest } from "../../models/equipmentTypes/equipmentType.ts";

export default defineComponent({
    name: "BikesPage",
    components: {
        BikeView,
        NewBikeForm
    },
    props: {
        user: {
            type: String,
            default: ""
        }
    },
    emits: {
        updateUsernameEvent() {
            return true;
        }
    },
    data: function () {
        return {
            bikes: [] as Array<Bike>,
            equipmentTypes: [] as Array<EquipmentType>
        };
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
});
</script>
