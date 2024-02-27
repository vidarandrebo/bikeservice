<template>
    <div>
        <button v-show="!show" @click="showForm">New Part</button>
        <form v-show="show" id="new-part" method="POST" @submit.prevent="addPart">
            <div>
                <label for="manufacturer">Manufacturer</label>
                <input id="manufacturer" v-model="partData.manufacturer" type="text" required />
            </div>
            <div>
                <label for="model">Model</label>
                <input id="model" v-model="partData.model" type="text" required />
            </div>
            <div>
                <label for="mileage">Mileage</label>
                <input id="mileage" v-model="partData.mileage" type="number" required />
            </div>
            <div>
                <label for="type">Type</label>
                <select id="type" v-model="partData.typeId" required>
                    <option value="0">No Type</option>
                    <option v-for="partType in partTypes" :key="partType.id" :value="partType.id">
                        {{ partType.name }}
                    </option>
                </select>
            </div>
            <div>
                <label for="type">Bike</label>
                <select id="type" v-model="partData.bikeId" required>
                    <option value="0">No Bike</option>
                    <option v-for="bike in bikes" :key="bike.id" :value="bike.id">
                        {{ bike.manufacturer }}
                        {{ bike.model }}
                    </option>
                </select>
            </div>
            <div>
                <input type="submit" value="Add" />
                <button @click="hideForm">Cancel</button>
            </div>
        </form>
    </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { Part } from "../../models/parts/part.ts";
import { EquipmentType } from "../../models/equipmentTypes/equipmentType.ts";
import { Bike } from "../../models/bikes/bike.ts";
import { Category } from "../../models/equipmentTypes/category.ts";

export default defineComponent({
    name: "NewPartForm",
    props: {
        equipmentTypes: {
            required: true,
            type: Array<EquipmentType>
        },
        bikes: {
            required: true,
            type: Array<Bike>
        }
    },
    emits: {
        updatePartsEvent() {
            return true;
        }
    },
    data: function () {
        return {
            partData: new Part(),
            show: false as boolean
        };
    },
    computed: {
        partTypes(): Array<EquipmentType> {
            return this.equipmentTypes.filter((t) => t.category == Category.Part);
        }
    },
    methods: {
        addPart: async function () {
            let result = await this.partData.addPartRequest();
            if (result.status == 201) {
                this.partData.clear();
                this.$emit("updatePartsEvent");
            }
        },
        hideForm: function () {
            this.show = false;
        },
        showForm: function () {
            this.show = true;
        }
    }
});
</script>
