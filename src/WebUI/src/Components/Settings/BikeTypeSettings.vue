<template>
    <div>
        <div>
            <h2>Add Bike Type</h2>
            <form id="addBikeType" method="POST" @submit.prevent="addType">
                <LabelPrimary for="name">Name</LabelPrimary>
                <InputText id="name" v-model="equipmentTypeSettings.name" name="name" required />
                <ButtonPrimary type="submit">Add Type"</ButtonPrimary>
            </form>
        </div>
        <div>
            <h2>Bike Types</h2>
            <p v-for="type in bikeTypes" :key="type.id">{{ type.name }}</p>
        </div>
    </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { EquipmentType, getTypeRequest } from "../../Models/EquipmentTypes/EquipmentType.ts";
import { Category } from "../../Models/EquipmentTypes/Category.ts";
import LabelPrimary from "../Common/LabelPrimary.vue";
import InputText from "../Common/InputText.vue";
import ButtonPrimary from "../Common/ButtonPrimary.vue";

export default defineComponent({
    name: "BikeTypeSettings",
    components: { ButtonPrimary, InputText, LabelPrimary },
    data: function () {
        return {
            equipmentTypeSettings: new EquipmentType(),
            equipmentTypes: [] as Array<EquipmentType>
        };
    },
    computed: {
        bikeTypes(): Array<EquipmentType> {
            return this.equipmentTypes.filter((t) => t.category == Category.Bike);
        }
    },
    async created() {
        await this.getTypes();
    },
    methods: {
        addType: async function () {
            let result = await this.equipmentTypeSettings.addTypeRequest();
            if (result.status == 201) {
                this.equipmentTypeSettings.clear();
            }
            await this.getTypes();
        },
        getTypes: async function () {
            this.equipmentTypes = await getTypeRequest();
        }
    }
});
</script>
