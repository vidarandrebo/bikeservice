<template>
    <div class="card">
        <div class="card-header">
            <h2 class="card-header-title">{{ bike.manufacturer }} {{ bike.model }}</h2>
        </div>
        <div class="card-content">
            <table class="table">
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
        </div>
    </div>
    <article class="article">
        <div class="buttons">
            <BButton v-show="showEditButton" type="is-primary" @click="showEdit">Edit</BButton>
            <BButton v-show="showEditButton" type="is-danger" @click="deleteBike">Delete</BButton>
        </div>
        <EditBikeForm
            v-show="showEditForm"
            :bike="bike"
            :equipmentTypes="equipmentTypes"
            @editDoneEvent="editDoneHandler"
            @updateBikesEvent="updateBikesHandler"
        ></EditBikeForm>
    </article>
</template>

<script setup lang="ts">
import { computed, ref } from "vue";
import EditBikeForm from "./EditBikeForm.vue";
import { Bike } from "../../Models/Bikes/Bike.ts";
import { EquipmentType } from "../../Models/EquipmentTypes/EquipmentType.ts";
import HeadingH2 from "../Common/Headings/HeadingH2.vue";
import { BButton } from "buefy";

const emit = defineEmits(["updateBikesEvent"]);

const showEditForm = ref(false);
const showEditButton = ref(true);

const props = defineProps<{
    bike: Bike;
    equipmentTypes: EquipmentType[];
}>();

const equipmentType = computed(() => {
    const type = props.equipmentTypes.find((t) => t.id == props.bike.typeId);
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
