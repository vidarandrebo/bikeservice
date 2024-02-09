<template>
    <div class="card">
        <form id="new-bike" method="POST" @submit.prevent="putBike">
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
                <input type="submit" value="Save" />
                <button @click.prevent="hideForm">Cancel</button>
            </div>
        </form>
    </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { Bike } from "../../models/bikes/bike.ts";
import { EquipmentType } from "../../models/equipmentTypes/equipmentType.ts";
import { Category } from "../../models/equipmentTypes/category.ts";
import { getDateString } from "../../models/dateFormatter.ts";

export default defineComponent({
    name: "EditBikeForm",
    props: {
        equipmentTypes: {
            required: true,
            type: Array<EquipmentType>
        },
        bike: {
            required: true,
            type: Bike
        }
    },
    emits: {
        updateBikesEvent() {
            return true;
        },
        editDoneEvent() {
            return true;
        }
    },
    data: function () {
        return {
            bikeData: new Bike(),
            date: ""
        };
    },
    computed: {
        bikeTypes(): Array<EquipmentType> {
            return this.equipmentTypes.filter((t) => t.category == Category.Bike);
        }
    },
    watch: {
        bike: function () {
            this.setFormDataFromProp();
        }
    },
    created: function () {
        this.setFormDataFromProp();
    },
    methods: {
        putBike: async function () {
            this.bikeData.date = new Date(this.date);
            let result = await this.bikeData.putBikeRequest();
            if (result.status === 200) {
                this.date = "";
                this.bikeData.clear();
                this.$emit("updateBikesEvent");
                this.$emit("editDoneEvent");
            }
        },
        hideForm: function () {
            this.$emit("editDoneEvent");
        },
        setFormDataFromProp() {
            this.bikeData = new Bike(this.bike);
            this.date = getDateString(this.bike.date);
        }
    }
});
</script>

<style scoped></style>
