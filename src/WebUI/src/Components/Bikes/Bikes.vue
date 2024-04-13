<template>
    <main>
        <article class="flex max-w-prose flex-col">
            <HeadingH1 class="">Bikes</HeadingH1>
            <NewBikeForm :equipment-types="equipmentTypes" @update-bikes-event="updateBikesHandler"></NewBikeForm>
            <ol class="space-y-2">
                <li v-for="bike in bikes" :key="bike.id">
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
import { Bike, getBikesRequest } from "../../Models/Bikes/Bike.ts";
import { EquipmentType, getTypeRequest } from "../../Models/EquipmentTypes/EquipmentType.ts";
import HeadingH1 from "../Common/Headings/HeadingH1.vue";

export default defineComponent({
    name: "BikesPage",
    components: {
        HeadingH1,
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
