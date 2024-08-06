<template>
    <article class="bg-gray-300 p-2 rounded">
        <table>
            <thead>
                <tr>
                    <th colspan="2" scope="row">
                        <HeadingH2>{{ bike.manufacturer }} {{ bike.model }}</HeadingH2>
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <th scope="row">
                        <p>Distance:</p>
                    </th>
                    <td>
                        <p>{{ bike.mileage }} km</p>
                    </td>
                </tr>
                <tr>
                    <th scope="row">
                        <p>Guid</p>
                    </th>
                    <td>
                        <p>{{ bike.id }}</p>
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
                        <p>Date</p>
                    </th>
                    <td>
                        <p>{{ bike.date }}</p>
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="space-x-1">
            <ButtonPrimary v-show="showEditButton" @click="showEdit">Edit</ButtonPrimary>
            <ButtonSecondary v-show="showEditButton" @click="deleteBike">Delete</ButtonSecondary>
        </div>
        <EditBikeForm
            v-show="showEditForm"
            :bike="bike"
            :equipment-types="equipmentTypes"
            @edit-done-event="editDoneHandler"
            @update-bikes-event="updateBikesHandler"
        ></EditBikeForm>
    </article>
</template>

<script setup lang="ts">
import { computed, ref } from "vue";
import EditBikeForm from "./EditBikeForm.vue";
import { Bike } from "../../Models/Bikes/Bike.ts";
import { EquipmentType } from "../../Models/EquipmentTypes/EquipmentType.ts";
import ButtonPrimary from "../Common/ButtonPrimary.vue";
import ButtonSecondary from "../Common/ButtonSecondary.vue";
import HeadingH2 from "../Common/Headings/HeadingH2.vue";

const emit = defineEmits(["updateBikesEvent"]);

const showEditForm = ref(false);
const showEditButton = ref(true);

const props = defineProps<{
    bike: Bike;
    equipmentTypes: EquipmentType[];
}>();

const equipmentType = computed(() => {
    let type = props.equipmentTypes.find((t) => t.id == props.bike.typeId);
    if (type != undefined) {
        return type;
    }
    return new EquipmentType();
});

async function deleteBike() {
    await props.bike.deleteBikeRequest();
    emit("updateBikesEvent");
}

function showEdit() {
    showEditForm.value = true;
    showEditButton.value = false;
}

function editDoneHandler() {
    showEditForm.value = false;
    showEditButton.value = true;
}

function updateBikesHandler() {
    emit("updateBikesEvent");
}
</script>
