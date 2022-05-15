<template>
    <div class="logregdiv">
        <form id="newbike" method="POST" v-on:submit.prevent="addBike">
            <input type="text" v-model="bikeData.manufacturer" required>
            <input type="text" v-model="bikeData.model" required>
            <input type="number" v-model="bikeData.mileage" required>
        </form>
    </div>
</template>

<script lang="ts">
import {BikeForm, IBikeForm} from "@/models/bike/bikeForm";
import {defineComponent} from "vue";

export default defineComponent({
    name: "NewBikeForm",
    data: function () {
        return {
            bikeData: new BikeForm() as IBikeForm,
        }
    },
    methods: {
        addBike: function () {
            return
        }
    }

})
</script>

<style scoped>

</style>