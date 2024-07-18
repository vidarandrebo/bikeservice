<template>
    <main>
        <article class="flex max-w-prose flex-col">
            <HeadingH1 class="">Bikes</HeadingH1>
            <NewBikeForm :equipment-types="equipmentTypes"></NewBikeForm>
            <ol class="space-y-2">
                <li v-for="bike in bikes" :key="bike.id">
                    <BikeView :bike="bike" :equipment-types="equipmentTypes"> </BikeView>
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
import { BikesDependency, DefaultBikeDependency } from "../../Models/Bikes/BikesDependency.ts";
import { DefaultEquipmentTypeDependency } from "../../Models/EquipmentTypes/EquipmentTypesDependency.ts";

const { bikes } = inject<BikesDependency>("bikes", DefaultBikeDependency, true);
const { equipmentTypes } = inject("equipmentTypes", DefaultEquipmentTypeDependency, true);
</script>
