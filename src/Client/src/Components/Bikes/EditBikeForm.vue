<template>
    <form id="new-bike" method="POST" @submit.prevent="putBike">
        <BField label="Manufacturer">
            <BInput id="manufacturer" v-model="bikeData.manufacturer" required />
        </BField>
        <BField label="Model">
            <BInput v-model="bikeData.model" required />
        </BField>
        <BField label="Mileage">
            <BNumberinput v-model="bikeData.mileage" required />
        </BField>
        <BField label="Date">
            <InputDate v-model="date" required />
        </BField>
        <BField label="Type">
            <SelectPrimary id="type" v-model="bikeData.typeId" required>
                <option value="0">No Type</option>
                <option v-for="bikeType in bikeTypes" :key="bikeType.id" :value="bikeType.id">
                    {{ bikeType.name }}
                </option>
            </SelectPrimary>
        </BField>
        <div class="buttons">
            <BButton type="is-primary" nativeType="submit">Save</BButton>
            <BButton type="is-primary" outlined @click.prevent="hideForm">Cancel</BButton>
        </div>
    </form>
</template>

<script setup lang="ts">
import { computed, onMounted, ref } from "vue";
import { Bike } from "../../Models/Bikes/Bike.ts";
import { EquipmentType } from "../../Models/EquipmentTypes/EquipmentType.ts";
import { Category } from "../../Models/EquipmentTypes/Category.ts";
import { getDateString } from "../../Models/DateFormatter.ts";
import SelectPrimary from "../Common/SelectPrimary.vue";
import InputDate from "../Common/InputDate.vue";
import { BButton, BField, BInput, BNumberinput } from "buefy";

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
