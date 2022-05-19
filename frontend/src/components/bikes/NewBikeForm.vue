<template>
    <div class="new-bike-view">
        <button v-on:click="showForm" v-show="!show">New Bike</button>
        <form id="new-bike" method="POST" v-on:submit.prevent="addBike" v-show="show">
            <label for="manufacturer">Manufacturer</label>
            <input type="text" id="manufacturer" v-model="bikeData.manufacturer" required>
            <label for="model">Model</label>
            <input type="text" id="model" v-model="bikeData.model" required>
            <label for="mileage">Mileage</label>
            <input type="number" id="mileage" v-model="bikeData.mileage" required>
            <input type="submit" value="Add">
            <button v-on:click="hideForm">Cancel</button>
        </form>
    </div>
</template>

<script lang="ts">
import {defineComponent} from "vue";
import {Bike} from "@/models/bikes/bike";

export default defineComponent({
    name: "NewBikeForm",
    data: function () {
        return {
            bikeData: new Bike(),
            show: false as boolean,
        }
    },
    emits: {
        updateBikesEvent() {
            return true
        }
    },
    methods: {
        addBike: async function () {
            let result = await this.bikeData.addBikeRequest();
            if (result.status == 201) {
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
    }

})
</script>

<style scoped>

</style>