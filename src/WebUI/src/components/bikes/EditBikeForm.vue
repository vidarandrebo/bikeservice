<template>
    <div class="card">
        <button v-on:click="showForm" v-show="!show">Edit</button>
        <form id="new-bike" method="POST" v-on:submit.prevent="putBike" v-show="show">
            <div class="form-field">
                <label for="manufacturer">Manufacturer</label>
                <input type="text" id="manufacturer" v-model="bikeData.manufacturer" required>
            </div>
            <div class="form-field">
                <label for="model">Model</label>
                <input type="text" id="model" v-model="bikeData.model" required>
            </div>
            <div class="form-field">
                <label for="mileage">Mileage</label>
                <input type="number" id="mileage" v-model="bikeData.mileage" required>
            </div>
            <div class="form-field">
                <label for="date">Date</label>
                <input type="date" id="date" v-model="date" required>
            </div>
            <div class="form-field">
                <label for="type">Type</label>
                <select id="type" v-model="bikeData.typeId" required>
                    <option value="0">No Type</option>
                    <option v-for="bikeType in bikeTypes" :value="bikeType.id" :key="bikeType.id">{{
                            bikeType.name
                        }}
                    </option>
                </select>
            </div>
            <div class="form-field">
                <input type="submit" value="Add">
                <button v-on:click="hideForm">Cancel</button>
            </div>
        </form>
    </div>
</template>

<script lang="ts">
import {defineComponent} from "vue";
import {Bike} from "@/models/bikes/bike";
import {IEquipmentType} from "@/models/equipmentTypes/equipmentType";
import {Category} from "@/models/equipmentTypes/category";

export default defineComponent({
    name: "EditBikeForm",
    data: function () {
        return {
            bikeData: new Bike(),
            date: "",
            show: false as boolean,
        }
    },
    props: {
        equipmentTypes: {
            required: true,
            type: Array<IEquipmentType>
        },
        bike: {
            required: true,
            type: Bike,
        }
    },
    emits: {
        updateBikesEvent() {
            return true
        }
    },
    computed: {
        bikeTypes(): Array<IEquipmentType> {
            return this.equipmentTypes.filter(t => t.category == Category.Bike);
        }
    },
    created: function () {
        this.bikeData = new Bike(this.bike);
    },
    methods: {
        putBike: async function () {
            this.bikeData.date = new Date(this.date);
            let result = await this.bikeData.putBikeRequest();
            if (result.status === 200) {
                this.date = "";
                this.bikeData.clear();
                this.$emit('updateBikesEvent');
            }
        },
        hideForm: function () {
            this.show = false;
        },
        showForm: function () {
            this.show = true;
        }
    },

})
</script>

<style scoped>
</style>
