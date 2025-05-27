<template>
    <form id="new-bike" method="POST" @submit.prevent="putBike">
        <FormField>
            <LabelPrimary for="manufacturer">Manufacturer</LabelPrimary>
            <InputText id="manufacturer" v-model="bikeData.manufacturer" required />
        </FormField>
        <FormField>
            <LabelPrimary for="model">Model</LabelPrimary>
            <InputText id="model" v-model="bikeData.model" required />
        </FormField>
        <FormField>
            <LabelPrimary for="mileage">Mileage</LabelPrimary>
            <InputNumber id="mileage" v-model="bikeData.mileage" required />
        </FormField>
        <FormField>
            <LabelPrimary for="date">Date</LabelPrimary>
            <InputDate id="date" v-model="date" required />
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
            <ButtonPrimary type="submit">Save</ButtonPrimary>
            <ButtonSecondary @click.prevent="hideForm">Cancel</ButtonSecondary>
        </FormField>
    </form>
</template>

<script setup lang="ts">
import { computed, onMounted, ref } from "vue";
import { Bike } from "../../Models/Bikes/Bike.ts";
import { EquipmentType } from "../../Models/EquipmentTypes/EquipmentType.ts";
import { Category } from "../../Models/EquipmentTypes/Category.ts";
import { getDateString } from "../../Models/DateFormatter.ts";
import ButtonPrimary from "../Common/ButtonPrimary.vue";
import ButtonSecondary from "../Common/ButtonSecondary.vue";
import LabelPrimary from "../Common/LabelPrimary.vue";
import SelectPrimary from "../Common/SelectPrimary.vue";
import InputText from "../Common/InputText.vue";
import InputNumber from "../Common/InputNumber.vue";
import InputDate from "../Common/InputDate.vue";
import FormField from "../Common/FormField.vue";

const props = defineProps<{
    equipmentTypes: EquipmentType[];
    bike: Bike;
}>();

const emit = defineEmits(["updateBikesEvent", "editDoneEvent"]);

const bikeData = ref(new Bike());
const date = ref("");

const bikeTypes = computed(() => {
    return props.equipmentTypes.filter((t) => t.category == Category.Bike);
});
onMounted(() => {
    bikeData.value = new Bike(props.bike);
    date.value = getDateString(props.bike.date);
});

async function putBike() {
    bikeData.value.date = new Date(date.value);
    const result = await bikeData.value.putBikeRequest();
    if (result === 200) {
        date.value = "";
        bikeData.value.clear();
        emit("updateBikesEvent");
        emit("editDoneEvent");
    }
}

function hideForm() {
    emit("editDoneEvent");
}
</script>
