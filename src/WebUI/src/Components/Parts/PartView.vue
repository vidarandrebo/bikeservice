<template>
    <article class="bg-gray-300 p-2 rounded">
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

<script setup lang="ts">
import { computed, ref } from "vue";
import EditPartForm from "./EditPartForm.vue";
import { Part } from "../../Models/Parts/Part.ts";
import { Bike } from "../../Models/Bikes/Bike.ts";
import { EquipmentType } from "../../Models/EquipmentTypes/EquipmentType.ts";
import ButtonPrimary from "../Common/ButtonPrimary.vue";
import ButtonSecondary from "../Common/ButtonSecondary.vue";
import HeadingH2 from "../Common/Headings/HeadingH2.vue";

const props = defineProps<{
    part: Part;
    bikes: Bike[];
    equipmentTypes: EquipmentType[];
}>();
const emit = defineEmits(["updatePartsEvent"]);
const showEditForm = ref(false);
const showEditButton = ref(true);

const bike = computed(() => {
    let bike = props.bikes.find((b) => b.id == props.part.bikeId);
    if (bike != undefined) {
        return bike;
    }
    return new Bike();
});
const equipmentType = computed(() => {
    let type = props.equipmentTypes.find((t) => t.id == props.part.typeId);
    if (type != undefined) {
        return type;
    }
    return new EquipmentType();
});

async function deletePart() {
    if (props.part != null) {
        await props.part.deletePartRequest();
        emit("updatePartsEvent");
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
    emit("updatePartsEvent");
}
</script>
