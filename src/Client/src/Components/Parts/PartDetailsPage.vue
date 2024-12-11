<script setup lang="ts">

import { computed, inject } from "vue";
import { BikeCollection, DefaultBikeCollection } from "../../Models/Bikes/BikeCollection.ts";
import { DefaultPartCollection, PartCollection } from "../../Models/Parts/PartCollection.ts";
import {
    DefaultEquipmentTypeCollection,
    EquipmentTypeCollection
} from "../../Models/EquipmentTypes/EquipmentTypeCollection.ts";
import { useRoute } from "vue-router";
import PartView from "./PartView.vue";

const { bikes } = inject<BikeCollection>("bikes", DefaultBikeCollection, true);
const { parts, fetchParts } = inject<PartCollection>("parts", DefaultPartCollection, true);
const { equipmentTypes } = inject<EquipmentTypeCollection>("equipmentTypes", DefaultEquipmentTypeCollection, true);

const route = useRoute();
const partID = route.params.id;
const part = computed(() => {
    return parts.value.find((p) => p.id == partID);
});

function onUpdatePartsEvent() {
    fetchParts();
}
</script>

<template>
    <PartView v-if="part" :part="part" :bikes="bikes" :equipmentTypes="equipmentTypes"
              @updatePartsEvent="onUpdatePartsEvent"></PartView>
</template>

<style scoped>

</style>