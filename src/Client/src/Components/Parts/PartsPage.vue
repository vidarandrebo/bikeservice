<template>
    <main>
        <article class="flex w-full max-w-screen-lg flex-col p-4">
            <HeadingH1 class="">Parts</HeadingH1>
            <NewPartForm :bikes="bikes" :equipmentTypes="equipmentTypes" @updatePartsEvent="onUpdatePartsEvent">
            </NewPartForm>
            <FormField class="flex flex-row space-x-1">
                <LabelPrimary for="selectBike">Bike</LabelPrimary>
                <SelectPrimary id="selectBike" v-model="bikeFilter">
                    <option value="0" selected>All Bikes</option>
                    <option v-for="bike in bikes" :key="bike.id" :value="bike.id">{{ bike.fullName }}</option>
                    /
                </SelectPrimary>
            </FormField>
            <ol class="space-y-2">
                <li v-for="part in parts" :key="part.id">
                    <RouterLink
                        v-if="bikeFilter == part.bikeId || bikeFilter == '0'"
                        :to="{ path: '/parts/' + part.id }"
                    >
                        <div class="grid grid-cols-3 hover:bg-gray-400 hover:rounded">
                            <p>{{ part.fullName }}</p>
                            <p>{{ bikes.find((b) => b.id == part.bikeId)?.fullName ?? "No Bike assigned" }}</p>
                            <p>{{ part.mileage }} km</p>
                        </div>
                    </RouterLink>
                </li>
            </ol>
        </article>
    </main>
</template>
<script setup lang="ts">
import NewPartForm from "./NewPartForm.vue";
import HeadingH1 from "../Common/Headings/HeadingH1.vue";
import { inject, ref } from "vue";
import {
    DefaultEquipmentTypeCollection,
    EquipmentTypeCollection
} from "../../Models/EquipmentTypes/EquipmentTypeCollection.ts";
import { BikeCollection, DefaultBikeCollection } from "../../Models/Bikes/BikeCollection.ts";
import { DefaultPartCollection, PartCollection } from "../../Models/Parts/PartCollection.ts";
import SelectPrimary from "../Common/SelectPrimary.vue";
import LabelPrimary from "../Common/LabelPrimary.vue";
import FormField from "../Common/FormField.vue";

function onUpdatePartsEvent() {
    fetchParts();
}

const { bikes } = inject<BikeCollection>("bikes", DefaultBikeCollection, true);
const { parts, fetchParts } = inject<PartCollection>("parts", DefaultPartCollection, true);
const { equipmentTypes } = inject<EquipmentTypeCollection>("equipmentTypes", DefaultEquipmentTypeCollection, true);

const bikeFilter = ref("0");
</script>
