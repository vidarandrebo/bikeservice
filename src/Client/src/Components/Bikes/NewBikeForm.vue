<template>
    <article>
        <BField>
            <BButton v-show="!show" type="is-primary" @click="showForm">New Bike</BButton>
        </BField>
        <form v-show="show" id="new-bike" method="POST" @submit.prevent="addBike">
            <BField label="Manufacturer">
                <BInput id="manufacturer" v-model="bikeData.manufacturer" required />
            </BField>
            <BField label="Model">
                <BInput id="model" v-model="bikeData.model" required />
            </BField>
            <BField label="Mileage">
                <BNumberinput id="mileage" v-model="bikeData.mileage" required></BNumberinput>
            </BField>
            <BField label="Date">
                <DateInput id="date" v-model="date" required />
            </BField>
            <BField label="Type">
                <SelectPrimary id="type" v-model="bikeData.typeId" required>
                    <option value="0">No Type</option>
                    <option v-for="bikeType in bikeTypes" :key="bikeType.id" :value="bikeType.id">
                        {{ bikeType.name }}
                    </option>
                </SelectPrimary>
            </BField>
            <BField>
                <BButton type="is-primary" @click="addBike">Add</BButton>
                <BButton type="is-primary is-outlined" @click="hideForm">Cancel</BButton>
            </BField>
        </form>
    </article>
</template>

<script setup lang="ts">
import { computed, ref } from "vue";
import { Bike } from "../../Models/Bikes/Bike.ts";
import { EquipmentType } from "../../Models/EquipmentTypes/EquipmentType.ts";
import { Category } from "../../Models/EquipmentTypes/Category.ts";
import DateInput from "../Common/InputDate.vue";
import SelectPrimary from "../Common/SelectPrimary.vue";
import { BButton, BField, BInput, BNumberinput } from "buefy";

const props = defineProps<{
    equipmentTypes: EquipmentType[];
}>();

const emit = defineEmits(["updateBikesEvent"]);

const bikeData = ref(new Bike());
const date = ref("");
const show = ref(false);

const bikeTypes = computed(() => {
    return props.equipmentTypes.filter((t) => t.category == Category.Bike);
});

async function addBike() {
    bikeData.value.date = new Date(date.value);
    const result = await bikeData.value.addBikeRequest();
    if (result === 201) {
        date.value = "";
        bikeData.value.clear();
        hideForm();
        emit("updateBikesEvent");
    }
}

function hideForm() {
    show.value = false;
}

function showForm() {
    show.value = true;
}
</script>
