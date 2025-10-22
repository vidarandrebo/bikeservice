<template>
    <main>
        <div class="container">
            <HeadingH1 class="">Parts</HeadingH1>
            <NewPartForm :bikes="bikes" :equipmentTypes="equipmentTypes" @updatePartsEvent="onUpdatePartsEvent">
            </NewPartForm>
            <BField label="Bike">
                <SelectPrimary id="selectBike" v-model="bikeFilter">
                    <option value="0" selected>All Bikes</option>
                    <option v-for="bike in bikes" :key="bike.id" :value="bike.id">{{ bike.fullName }}</option>
                    /
                </SelectPrimary>
            </BField>
            <BTable :data="parts" hoverable @dblclick="onRowDoubleClick">
                <BTableColumn v-slot="slotProps" label="Manufacturer">
                    {{ slotProps.row.manufacturer }}
                </BTableColumn>
                <BTableColumn v-slot="slotProps" label="Model">
                    {{ slotProps.row.model }}
                </BTableColumn>
                <BTableColumn v-slot="slotProps" label="Type">
                    {{ equipmentTypes.find((t) => t.id == slotProps.row.typeId)?.name ?? "No type assigned" }}
                </BTableColumn>
                <BTableColumn v-slot="slotProps" label="Mileage">
                    {{ slotProps.row.mileage }}
                </BTableColumn>
                <BTableColumn v-slot="slotProps" label="Bike">
                    {{ bikes.find((b) => b.id == slotProps.row.bikeId)?.fullName ?? "No Bike assigned" }}
                </BTableColumn>
            </BTable>
        </div>
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
import { BField, BTable, BTableColumn } from "buefy";
import { Part } from "../../Models/Parts/Part.ts";
import router from "../../Router";

function onUpdatePartsEvent() {
    fetchParts();
}

function onRowDoubleClick(part: Part) {
    router.push(`/parts/${part.id}`);
}

const { bikes } = inject<BikeCollection>("bikes", DefaultBikeCollection, true);
const { parts, fetchParts } = inject<PartCollection>("parts", DefaultPartCollection, true);
const { equipmentTypes } = inject<EquipmentTypeCollection>("equipmentTypes", DefaultEquipmentTypeCollection, true);

const bikeFilter = ref("0");
</script>
