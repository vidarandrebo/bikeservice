<script setup lang="ts">
import { onMounted, provide, ref } from "vue";
import { Bike, getBikesRequest } from "./Models/Bikes/Bike.ts";
import { getPartsRequest, Part } from "./Models/Parts/Part.ts";
import { EquipmentType, getTypeRequest } from "./Models/EquipmentTypes/EquipmentType.ts";
import { loadUserFromLocalStorage, User } from "./Models/Auth/User.ts";

const user = ref<User>(new User());
const bikes = ref<Bike[]>([]);
const parts = ref<Part[]>([]);
const equipmentTypes = ref<EquipmentType[]>([]);

provide("user", { user, setUser });
provide("bikes", { bikes, addBike, fetchBikes });
provide("parts", { parts, fetchParts });
provide("equipmentTypes", { equipmentTypes, fetchEquipmentTypes });

function setBikes(value: Bike[]) {
    value.sort((a, b) => {
        const aName = a.fullName.toLowerCase();
        const bName = b.fullName.toLowerCase();
        if (aName > bName) {
            return 1;
        } else if (aName < bName) {
            return -1;
        }
        return 0;
    });
    bikes.value = value;
}


function addBike(value: Bike) {
    bikes.value.push(value);
}

function setParts(value: Part[]) {
    value.sort((a, b) => {
        const aName = a.fullName.toLowerCase();
        const bName = b.fullName.toLowerCase();
        if (aName > bName) {
            return 1;
        } else if (aName < bName) {
            return -1;
        }
        return 0;
    });
    parts.value = value;
}

function setUser(value: User) {
    user.value = value;
}

function setEquipmentTypes(value: EquipmentType[]) {
    equipmentTypes.value = value;
}

onMounted(() => {
    const storedUser = loadUserFromLocalStorage();
    if (storedUser) {
        storedUser.refresh().then((u) => {
            if (u) {
                setUser(u);
            }
        });
    }
    fetchBikes();
    fetchParts();
    fetchEquipmentTypes();
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
