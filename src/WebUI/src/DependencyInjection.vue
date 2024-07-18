<script setup lang="ts">
import { onMounted, provide, ref } from "vue";
import { Bike, getBikesRequest } from "./Models/Bikes/Bike.ts";
import { getPartsRequest, Part } from "./Models/Parts/Part.ts";
import { EquipmentType, getTypeRequest } from "./Models/EquipmentTypes/EquipmentType.ts";

const user = ref<string>("");
const bikes = ref<Bike[]>([]);
const parts = ref<Part[]>([]);
const equipmentTypes = ref<EquipmentType[]>([]);

provide("user", user);
provide("bikes", { bikes, addBike, setBikes });
provide("parts", { parts, setParts });
provide("equipmentTypes", { equipmentTypes, setEquipmentTypes });

function setBikes(value: Bike[]) {
    bikes.value = value;
}

function addBike(value: Bike) {
    bikes.value.push(value);
}

function setParts(value: Part[]) {
    parts.value = value;
}

function setEquipmentTypes(value: EquipmentType[]) {
    equipmentTypes.value = value;
}

onMounted(async () => {
    setBikes(await getBikesRequest());
    setParts(await getPartsRequest());
    setEquipmentTypes(await getTypeRequest());
});
</script>

<template>
    <slot></slot>
</template>
