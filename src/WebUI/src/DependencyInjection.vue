<script setup lang="ts">
import { onMounted, provide, ref } from "vue";
import { Bike, getBikesRequest } from "./Models/Bikes/Bike.ts";
import { getPartsRequest, Part } from "./Models/Parts/Part.ts";
import { EquipmentType, getTypeRequest } from "./Models/EquipmentTypes/EquipmentType.ts";
import { httpGetWithBody } from "./Models/HttpMethods.ts";
import { User } from "./Models/Auth/User.ts";
import { AuthRouteResponse } from "./Models/Auth/AuthRouteResponse.ts";

const user = ref<User>(new User(""));
const bikes = ref<Bike[]>([]);
const parts = ref<Part[]>([]);
const equipmentTypes = ref<EquipmentType[]>([]);

provide("user", { user, setUser });
provide("bikes", { bikes, addBike, fetchBikes });
provide("parts", { parts, fetchParts });
provide("equipmentTypes", { equipmentTypes, fetchEquipmentTypes });

function setBikes(value: Bike[]) {
    bikes.value = value;
}

function addBike(value: Bike) {
    bikes.value.push(value);
}

function setParts(value: Part[]) {
    parts.value = value;
}

function setUser(value: User) {
    user.value = value;
}

function setEquipmentTypes(value: EquipmentType[]) {
    equipmentTypes.value = value;
}

onMounted(() => {
    fetchBikes();
    fetchParts();
    fetchEquipmentTypes();
    httpGetWithBody<AuthRouteResponse>("/api/login").then((u) => setUser(new User(u.body.userName)));
});

function fetchBikes() {
    getBikesRequest().then((b) => setBikes(b));
}

function fetchParts() {
    getPartsRequest().then((p) => setParts(p));
}

function fetchEquipmentTypes() {
    getTypeRequest().then((t) => setEquipmentTypes(t));
}
</script>

<template>
    <slot></slot>
</template>
