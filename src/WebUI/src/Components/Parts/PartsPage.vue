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
    DefaultEquipmentTypeCollection,
    EquipmentTypeCollection
} from "../../Models/EquipmentTypes/EquipmentTypeCollection.ts";
import { BikeCollection, DefaultBikeCollection } from "../../Models/Bikes/BikeCollection.ts";
import { DefaultPartCollection, PartCollection } from "../../Models/Parts/PartCollection.ts";

function updatePartsHandler() {}

const { bikes } = inject<BikeCollection>("bikes", DefaultBikeCollection, true);
const { parts } = inject<PartCollection>("parts", DefaultPartCollection, true);
const { equipmentTypes } = inject<EquipmentTypeCollection>("equipmentTypes", DefaultEquipmentTypeCollection, true);
</script>
