<template>
    <div class="bike-view">
        <h5>{{ bike.manufacturer }} {{ bike.model }}</h5>
        <div class="bike-specs">
            <div class="spec">
                <p>Distance:</p>
                <p>{{ bike.mileage }} km</p>
            </div>
            <div class="spec">
                <p>Guid:</p>
                <p>{{ bike.id }}</p>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import {Bike} from "@/models/bikes/bike";
import {defineComponent, PropType} from "vue";

export default defineComponent({
    name: "BikeView",
    props: {
        bike: {
            type: Object as PropType<Bike>
        }
    },
})
</script>


<style scoped>

</style>