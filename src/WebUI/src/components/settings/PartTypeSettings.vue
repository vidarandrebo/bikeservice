<template>
    <div>
        <div>
            <h2>Add Part Type</h2>
            <form id="addBikeType" method="POST" @submit.prevent="addType">
                <LabelPrimary for="name">Name</LabelPrimary>
                <InputText id="name" v-model="equipmentTypeSettings.name" name="name" required />
                <ButtonPrimary type="submit">Add Type"</ButtonPrimary>
            </form>
        </div>
        <div>
            <h2>Part Types</h2>
            <p v-for="type in partTypes" :key="type.id">{{ type.name }}</p>
        </div>
    </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { EquipmentType, getTypeRequest } from "../../models/equipmentTypes/equipmentType.ts";
import { Category } from "../../models/equipmentTypes/category.ts";
import LabelPrimary from "../common/LabelPrimary.vue";
import InputText from "../common/InputText.vue";
import ButtonPrimary from "../common/ButtonPrimary.vue";

export default defineComponent({
    name: "PartTypeSettings",
    components: { ButtonPrimary, InputText, LabelPrimary },
    data: function () {
        return {
            equipmentTypeSettings: new EquipmentType(),
            equipmentTypes: [] as Array<EquipmentType>
        };
    },
    computed: {
        partTypes(): Array<EquipmentType> {
            return this.equipmentTypes.filter((t) => t.category == Category.Part);
        }
    },
    async created() {
        await this.getTypes();
    },
    methods: {
        addType: async function () {
            this.equipmentTypeSettings.category = Category.Part;
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
