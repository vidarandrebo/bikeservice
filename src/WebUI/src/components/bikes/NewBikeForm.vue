<template>
    <div class="card">
        <button v-show="!show" @click="showForm">New Bike</button>
        <form v-show="show" id="new-bike" method="POST" @submit.prevent="addBike">
            <div class="form-field">
                <label for="manufacturer">Manufacturer</label>
                <input id="manufacturer" v-model="bikeData.manufacturer" type="text" required />
            </div>
            <div class="form-field">
                <label for="model">Model</label>
                <input id="model" v-model="bikeData.model" type="text" required />
            </div>
            <div class="form-field">
                <label for="mileage">Mileage</label>
                <input id="mileage" v-model="bikeData.mileage" type="number" required />
            </div>
            <div class="form-field">
                <label for="date">Date</label>
                <input id="date" v-model="date" type="date" required />
            </div>
            <div class="form-field">
                <label for="type">Type</label>
                <select id="type" v-model="bikeData.typeId" required>
                    <option value="0">No Type</option>
                    <option v-for="bikeType in bikeTypes" :key="bikeType.id" :value="bikeType.id">
                        {{ bikeType.name }}
                    </option>
                </select>
            </div>
            <div class="form-field">
                <input type="submit" value="Add" />
                <button @click="hideForm">Cancel</button>
            </div>
        </form>
    </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { Bike } from "../../models/bikes/bike.ts";
import { EquipmentType } from "../../models/equipmentTypes/equipmentType.ts";
import { Category } from "../../models/equipmentTypes/category.ts";

export default defineComponent({
    name: "NewBikeForm",
    props: {
        equipmentTypes: {
            required: true,
            type: Array<EquipmentType>
        }
    },
    emits: {
        updateBikesEvent() {
            return true;
        }
    },
    data: function () {
        return {
            bikeData: new Bike(),
            date: "",
            show: false as boolean
        };
    },
    computed: {
        bikeTypes(): Array<EquipmentType> {
            return this.equipmentTypes.filter((t) => t.category == Category.Bike);
        }
    },
    methods: {
        addBike: async function () {
            this.bikeData.date = new Date(this.date);
            let result = await this.bikeData.addBikeRequest();
            if (result.status === 201) {
                this.date = "";
                this.bikeData.clear();
                this.$emit("updateBikesEvent");
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

<style scoped></style>
