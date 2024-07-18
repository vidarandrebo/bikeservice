<template>
    <main>
        <article class="flex max-w-prose flex-col">
            <HeadingH1 class="">Parts</HeadingH1>
            <NewPartForm :bikes="bikes" :equipment-types="equipmentTypes" @update-parts-event="updatePartsHandler">
            </NewPartForm>
            <ol class="space-y-2">
                <li v-for="part in parts" :key="part.id">
                    <PartView
                        :bikes="bikes"
                        :equipment-types="equipmentTypes"
                        :part="part"
                        @update-parts-event="updatePartsHandler"
                    >
                    </PartView>
                </li>
            </ol>
        </article>
    </main>
</template>
<script setup lang="ts">
import PartView from "./PartView.vue";
import NewPartForm from "./NewPartForm.vue";
import HeadingH1 from "../Common/Headings/HeadingH1.vue";
import { inject } from "vue";
import {
    DefaultEquipmentTypeDependency,
    EquipmentTypeDependency
} from "../../Models/EquipmentTypes/EquipmentTypesDependency.ts";
import { BikesDependency, DefaultBikeDependency } from "../../Models/Bikes/BikesDependency.ts";
import { DefaultPartsDependency, PartsDependency } from "../../Models/Parts/PartsDependency.ts";

function updatePartsHandler() {}

const { bikes } = inject<BikesDependency>("bikes", DefaultBikeDependency, true);
const { parts } = inject<PartsDependency>("parts", DefaultPartsDependency, true);
const { equipmentTypes } = inject<EquipmentTypeDependency>("equipmentTypes", DefaultEquipmentTypeDependency, true);
</script>
