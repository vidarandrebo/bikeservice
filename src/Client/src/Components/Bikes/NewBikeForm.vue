<template>
    <article>
        <FormField>
            <ButtonPrimary v-show="!show" @click="showForm">New Bike</ButtonPrimary>
        </FormField>
        <form v-show="show" id="new-bike" method="POST" @submit.prevent="addBike">
            <FormField>
                <LabelPrimary for="manufacturer">Manufacturer</LabelPrimary>
                <TextInput id="manufacturer" v-model="bikeData.manufacturer" required />
            </FormField>
            <FormField>
                <LabelPrimary for="model">Model</LabelPrimary>
                <TextInput id="model" v-model="bikeData.model" required />
            </FormField>
            <FormField>
                <LabelPrimary for="mileage">Mileage</LabelPrimary>
                <NumberInput id="mileage" v-model="bikeData.mileage" required />
            </FormField>
            <FormField>
                <LabelPrimary for="date">Date</LabelPrimary>
                <DateInput id="date" v-model="date" required />
            </FormField>
            <FormField>
                <LabelPrimary for="type">Type</LabelPrimary>
                <SelectPrimary id="type" v-model="bikeData.typeId" required>
                    <option value="0">No Type</option>
                    <option v-for="bikeType in bikeTypes" :key="bikeType.id" :value="bikeType.id">
                        {{ bikeType.name }}
                    </option>
                </SelectPrimary>
            </FormField>
            <FormField class="space-x-1">
                <ButtonPrimary type="submit">Add</ButtonPrimary>
                <ButtonSecondary @click="hideForm">Cancel</ButtonSecondary>
            </FormField>
        </form>
    </article>
</template>

<script setup lang="ts">
import { computed, ref } from "vue";
import { Bike } from "../../Models/Bikes/Bike.ts";
import { EquipmentType } from "../../Models/EquipmentTypes/EquipmentType.ts";
import { Category } from "../../Models/EquipmentTypes/Category.ts";
import ButtonPrimary from "../Common/ButtonPrimary.vue";
import LabelPrimary from "../Common/LabelPrimary.vue";
import TextInput from "../Common/InputText.vue";
import NumberInput from "../Common/InputNumber.vue";
import DateInput from "../Common/InputDate.vue";
import SelectPrimary from "../Common/SelectPrimary.vue";
import ButtonSecondary from "../Common/ButtonSecondary.vue";
import FormField from "../Common/FormField.vue";

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
    let result = await bikeData.value.addBikeRequest();
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
