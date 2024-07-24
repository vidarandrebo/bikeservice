<template>
    <main>
        <article class="flex max-w-prose flex-col">
            <HeadingH1 class="">Bikes</HeadingH1>
            <NewBikeForm :equipmentTypes="equipmentTypes" @updateBikesEvent="onUpdateBikesEvent"></NewBikeForm>
            <ol class="space-y-2">
                <li v-for="bike in bikes" :key="bike.id">
                    <BikeView
                        :equipmentTypes="equipmentTypes"
                        :bike="bike"
                        @updateBikesEvent="onUpdateBikesEvent"
                    ></BikeView>
                </li>
            </ol>
        </article>
    </main>
</template>
<script setup lang="ts">
import { inject } from "vue";
import BikeView from "./BikeView.vue";
import NewBikeForm from "./NewBikeForm.vue";
import HeadingH1 from "../Common/Headings/HeadingH1.vue";
import { BikeCollection, DefaultBikeCollection } from "../../Models/Bikes/BikeCollection.ts";
import { DefaultEquipmentTypeCollection } from "../../Models/EquipmentTypes/EquipmentTypeCollection.ts";

const { bikes, fetchBikes } = inject<BikeCollection>("bikes", DefaultBikeCollection, true);
const { equipmentTypes } = inject("equipmentTypes", DefaultEquipmentTypeCollection, true);

function onUpdateBikesEvent() {
    fetchBikes();
}
</script>
