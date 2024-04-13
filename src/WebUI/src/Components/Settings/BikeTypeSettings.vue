<template>
    <article>
        <div class="flex flex-col items-center grow">
            <div>
                <HeadingH2>Add Bike Type</HeadingH2>
                <form id="addBikeType" method="POST" @submit.prevent="addType">
                    <FormField>
                        <LabelPrimary for="name">Name</LabelPrimary>
                        <InputText id="name" v-model="equipmentTypeSettings.name" name="name" required />
                    </FormField>
                    <FormField>
                        <ButtonPrimary type="submit">Add Type</ButtonPrimary>
                    </FormField>
                </form>
                <HeadingH3>Bike Types</HeadingH3>
                <p v-for="type in bikeTypes" :key="type.id">{{ type.name }}</p>
            </div>
        </div>
    </article>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { EquipmentType, getTypeRequest } from "../../Models/EquipmentTypes/EquipmentType.ts";
import { Category } from "../../Models/EquipmentTypes/Category.ts";
import LabelPrimary from "../Common/LabelPrimary.vue";
import InputText from "../Common/InputText.vue";
import ButtonPrimary from "../Common/ButtonPrimary.vue";
import HeadingH2 from "../Common/Headings/HeadingH2.vue";
import HeadingH3 from "../Common/Headings/HeadingH3.vue";
import FormField from "../Common/FormField.vue";

export default defineComponent({
    name: "BikeTypeSettings",
    components: { FormField, HeadingH2, HeadingH3, ButtonPrimary, InputText, LabelPrimary },
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
