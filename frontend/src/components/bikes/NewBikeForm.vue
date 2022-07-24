<template>
    <div class="new-bike-view">
        <button v-on:click="showForm" v-show="!show">New Bike</button>
        <form id="new-bike" method="POST" v-on:submit.prevent="addBike" v-show="show">
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
                <input type="date" id="date" v-model="bikeData.date" required>
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
    name: "NewBikeForm",
    data: function () {
        return {
            bikeData: new Bike(),
            show: false as boolean,
        }
    },
    props: {
        equipmentTypes: {
            required: true,
            type: Array<IEquipmentType>
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
    methods: {
        addBike: async function () {
            let result = await this.bikeData.addBikeRequest();
            if (result.status === 201) {
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
.new-bike-view {
    background-color: beige;
    margin: 0.5rem;
    padding: 1rem;
}

</style>
