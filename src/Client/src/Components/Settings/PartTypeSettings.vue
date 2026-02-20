<template>
    <article>
        <div class="">
            <div>
                <HeadingH2>Add Part Type</HeadingH2>
                <form id="addBikeType" method="POST" @submit.prevent="addType">
                    <BField label="Name">
                        <InputText id="name" v-model="equipmentTypeSettings.name" name="name" required />
                    </BField>
                    <BField>
                        <BButton type="is-primary" nativeType="submit">Add Type</BButton>
                    </BField>
                </form>
                <HeadingH3>Part Types</HeadingH3>
                <ul>
                    <li v-for="type in partTypes" :key="type.id">{{ type.name }}</li>
                </ul>
            </div>
        </div>
    </article>
</template>

<script setup lang="ts">
import { computed, inject, ref } from "vue";
import { EquipmentType } from "../../Models/EquipmentTypes/EquipmentType.ts";
import { Category } from "../../Models/EquipmentTypes/Category.ts";
import InputText from "../Common/InputText.vue";
import HeadingH2 from "../Common/Headings/HeadingH2.vue";
import HeadingH3 from "../Common/Headings/HeadingH3.vue";
import { DefaultEquipmentTypeCollection } from "../../Models/EquipmentTypes/EquipmentTypeCollection.ts";
import { BButton, BField } from "buefy";

const equipmentTypeSettings = ref(new EquipmentType());
const { equipmentTypes, fetchEquipmentTypes } = inject("equipmentTypes", DefaultEquipmentTypeCollection, true);

const partTypes = computed(() => {
    return equipmentTypes.value.filter((t) => t.category == Category.Part);
});

async function addType() {
    equipmentTypeSettings.value.category = Category.Part;
    const result = await equipmentTypeSettings.value.addTypeRequest();
    if (result === 201) {
        equipmentTypeSettings.value.clear();
    }
    fetchEquipmentTypes();
}
</script>
