<template>
    <form id="edit-part" method="POST" @submit.prevent="putPart">
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
            <BButton type="is-primary">Save</BButton>
            <BButton type="is-primary" inverted @click.prevent="hideForm">Cancel</BButton>
        </BField>
    </form>
</template>

<script setup lang="ts">
import { computed, onMounted, ref } from "vue";
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
    part: Part;
    bikes: Bike[];
}>();
const partData = ref(new Part());
const emit = defineEmits(["updatePartsEvent", "editDoneEvent"]);
const partTypes = computed(() => {
    return props.equipmentTypes.filter((t) => t.category == Category.Part);
});

async function putPart() {
    const result = await partData.value.putPartRequest();
    if (result === 200) {
        partData.value.clear();
        emit("updatePartsEvent");
        emit("editDoneEvent");
    }
}

function hideForm() {
    emit("editDoneEvent");
}

onMounted(() => {
    partData.value = new Part(props.part);
});
</script>
