<template>
    <form id="new-bike" method="POST" @submit.prevent="putBike">
        <BField>
            <LabelPrimary for="manufacturer">Manufacturer</LabelPrimary>
            <InputText id="manufacturer" v-model="bikeData.manufacturer" required />
        </BField>
        <BField>
            <LabelPrimary for="model">Model</LabelPrimary>
            <InputText id="model" v-model="bikeData.model" required />
        </BField>
        <BField>
            <LabelPrimary for="mileage">Mileage</LabelPrimary>
            <InputNumber id="mileage" v-model="bikeData.mileage" required />
        </BField>
        <BField>
            <LabelPrimary for="date">Date</LabelPrimary>
            <InputDate id="date" v-model="date" required />
        </BField>
        <BField>
            <LabelPrimary for="type">Type</LabelPrimary>
            <SelectPrimary id="type" v-model="bikeData.typeId" required>
                <option value="0">No Type</option>
                <option v-for="bikeType in bikeTypes" :key="bikeType.id" :value="bikeType.id">
                    {{ bikeType.name }}
                </option>
            </SelectPrimary>
        </BField>
        <BField>
            <BButton type="is-primary">Save</BButton>
            <BButton type="is-primary" inverted @click.prevent="hideForm">Cancel</BButton>
        </BField>
    </form>
</template>

<script setup lang="ts">
import { computed, onMounted, ref } from "vue";
import { Bike } from "../../Models/Bikes/Bike.ts";
import { EquipmentType } from "../../Models/EquipmentTypes/EquipmentType.ts";
import { Category } from "../../Models/EquipmentTypes/Category.ts";
import { getDateString } from "../../Models/DateFormatter.ts";
import SelectPrimary from "../Common/SelectPrimary.vue";
import InputText from "../Common/InputText.vue";
import InputNumber from "../Common/InputNumber.vue";
import InputDate from "../Common/InputDate.vue";
import { BButton, BField } from "buefy";

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
