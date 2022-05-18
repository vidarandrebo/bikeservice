<template>
    <div class="authentication">
        <form id="newbike" method="POST" v-on:submit.prevent="addBike">
            <label for="manufacturer">Manufacturer</label>
            <input type="text" id="manufacturer" v-model="bikeData.manufacturer" required>
            <label for="model">Model</label>
            <input type="text" id="model" v-model="bikeData.model" required>
            <label for="mileage">Mileage</label>
            <input type="number" id="mileage" v-model="bikeData.mileage" required>
            <input type="submit" value="Add">
        </form>
    </div>
</template>

<script lang="ts">
import {BikeForm} from "@/models/bikes/bikeForm";
import {defineComponent} from "vue";

export default defineComponent({
    name: "NewBikeForm",
    data: function () {
        return {
            bikeData: new BikeForm(),
        }
    },
    methods: {
        addBike: async function () {
            let result = await this.bikeData.addBikeRequest();
            if (result.status == 201) {
                this.bikeData.clear();
            }
        }
    }

})
</script>

<style scoped>

</style>