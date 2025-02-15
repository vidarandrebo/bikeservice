<script setup lang="ts">
import { computed, inject, ref } from "vue";
import { BikeCollection, DefaultBikeCollection } from "../../Models/Bikes/BikeCollection.ts";
import { DefaultPartCollection, PartCollection } from "../../Models/Parts/PartCollection.ts";
import {
    DefaultEquipmentTypeCollection,
    EquipmentTypeCollection
} from "../../Models/EquipmentTypes/EquipmentTypeCollection.ts";
import { useRoute } from "vue-router";
import EditPartForm from "./EditPartForm.vue";
import ButtonSecondary from "../Common/ButtonSecondary.vue";
import ButtonPrimary from "../Common/ButtonPrimary.vue";
import HeadingH2 from "../Common/Headings/HeadingH2.vue";
import { Bike } from "../../Models/Bikes/Bike.ts";
import { EquipmentType } from "../../Models/EquipmentTypes/EquipmentType.ts";
import router from "../../Router";

const { bikes } = inject<BikeCollection>("bikes", DefaultBikeCollection, true);
const { parts, fetchParts } = inject<PartCollection>("parts", DefaultPartCollection, true);
const { equipmentTypes } = inject<EquipmentTypeCollection>("equipmentTypes", DefaultEquipmentTypeCollection, true);

const route = useRoute();
const partID = route.params.id;
const part = computed(() => {
    const part = parts.value.find((p) => p.id == partID);
    if (part == undefined) {
        router.replace({ path: "/parts" });
    }
    return part;
});

const showEditForm = ref(false);
const showEditButton = ref(true);

const bike = computed(() => {
    let bike = bikes.value.find((b) => b.id == part.value?.bikeId);
    if (bike != undefined) {
        return bike;
    }
    return new Bike();
});
const equipmentType = computed(() => {
    let type = equipmentTypes.value.find((t) => t.id == part.value?.typeId);
    if (type != undefined) {
        return type;
    }
    return new EquipmentType();
});

async function deletePart() {
    if (part.value != null) {
        await part.value.deletePartRequest();
        await router.push({ path: "/parts" });
        fetchParts();
    }
}

function showEdit() {
    showEditForm.value = true;
    showEditButton.value = false;
}

function editDoneHandler() {
    showEditForm.value = false;
    showEditButton.value = true;
}

function updatePartsHandler() {
    fetchParts();
}
</script>

<template>
    <article v-if="part" class="">
        <table>
            <thead>
                <tr>
                    <th colspan="2" scope="row">
                        <HeadingH2>{{ part.manufacturer }} {{ part.model }}</HeadingH2>
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <th scope="row">
                        <p>Distance:</p>
                    </th>
                    <td>
                        <p>{{ part.mileage }} km</p>
                    </td>
                </tr>
                <tr>
                    <th scope="row">
                        <p>Guid</p>
                    </th>
                    <td>
                        <p>{{ part.id }}</p>
                    </td>
                </tr>
                <tr>
                    <th scope="row">
                        <p>Type</p>
                    </th>
                    <td>
                        <p>{{ equipmentType.name }}</p>
                    </td>
                </tr>
                <tr>
                    <th scope="row">
                        <p>Bike</p>
                    </th>
                    <td>
                        <p>{{ bike.manufacturer }} {{ bike.model }}</p>
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="space-x-1">
            <ButtonPrimary v-show="showEditButton" @click="showEdit">Edit</ButtonPrimary>
            <ButtonSecondary v-show="showEditButton" @click="deletePart">Delete</ButtonSecondary>
        </div>
        <EditPartForm
            v-show="showEditForm"
            :bikes="bikes"
            :equipment-types="equipmentTypes"
            :part="part"
            @edit-done-event="editDoneHandler"
            @update-parts-event="updatePartsHandler"
        >
        </EditPartForm>
    </article>
</template>

<style scoped></style>
