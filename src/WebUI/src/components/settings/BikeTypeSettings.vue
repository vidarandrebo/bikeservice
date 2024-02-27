<template>
    <div>
        <div>
            <h2>Add Bike Type</h2>
            <form id="addBikeType" method="POST" @submit.prevent="addType">
                <label for="name">Name</label>
                <input id="name" v-model="equipmentTypeSettings.name" type="text" name="name" required />
                <input type="submit" value="Add Type" />
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
import { EquipmentType, getTypeRequest } from "../../models/equipmentTypes/equipmentType.ts";
import { Category } from "../../models/equipmentTypes/category.ts";

export default defineComponent({
    name: "BikeTypeSettings",
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
