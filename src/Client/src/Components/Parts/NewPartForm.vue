<template>
    <article>
        <BField>
            <BButton v-show="!show" type="is-primary" @click="showForm">New Part</BButton>
        </BField>
        <form v-show="show" id="new-part" method="POST" @submit.prevent="addPart">
            <BField label="Manufacturer">
                <InputText id="manufacturer" v-model="partData.manufacturer" required />
            </BField>
            <BField label="Model">
                <InputText id="model" v-model="partData.model" required />
            </BField>
            <BField label="Mileage">
                <InputNumber id="mileage" v-model="partData.mileage" required />
            </BField>
            <BField label="Type">
                <SelectPrimary id="type" v-model="partData.typeId" required>
                    <option value="0">No Type</option>
                    <option v-for="partType in partTypes" :key="partType.id" :value="partType.id">
                        {{ partType.name }}
                    </option>
                </SelectPrimary>
            </BField>
            <BField label="Bike">
                <SelectPrimary id="type" v-model="partData.bikeId" required>
                    <option value="0">No Bike</option>
                    <option v-for="bike in bikes" :key="bike.id" :value="bike.id">
                        {{ bike.manufacturer }}
                        {{ bike.model }}
                    </option>
                </SelectPrimary>
            </BField>
            <BField>
                <BButton type="is-primary">Add</BButton>
                <BButton type="is-primary is-outlined" @click="hideForm">Cancel</BButton>
            </BField>
        </form>
    </article>
</template>

<script setup lang="ts">
import { computed, ref } from "vue";
import { Part } from "../../Models/Parts/Part.ts";
import { EquipmentType } from "../../Models/EquipmentTypes/EquipmentType.ts";
import { Bike } from "../../Models/Bikes/Bike.ts";
import { Category } from "../../Models/EquipmentTypes/Category.ts";
import InputText from "../Common/InputText.vue";
import InputNumber from "../Common/InputNumber.vue";
import SelectPrimary from "../Common/SelectPrimary.vue";
import { BButton, BField } from "buefy";

const props = defineProps<{
    equipmentTypes: EquipmentType[];
    bikes: Bike[];
}>();
const emit = defineEmits(["updatePartsEvent"]);
const partData = ref(new Part());
const show = ref(false);
const partTypes = computed(() => {
    return props.equipmentTypes.filter((t) => t.category == Category.Part);
});

async function addPart() {
    const result = await partData.value.addPartRequest();
    if (result) {
        partData.value.clear();
        emit("updatePartsEvent");
    }
}

function hideForm() {
    show.value = false;
}

function showForm() {
    show.value = true;
}
</script>
