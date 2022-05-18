<template>
    <div class="post">
        <table>
            <thead>
            <tr>
                <th>Manufacturer</th>
                <th>Model</th>
                <th>Km</th>
            </tr>
            </thead>
            <tbody>
            <tr v-for="bike in bikes">
                <td>{{ bike.manufacturer }}</td>
                <td>{{ bike.model }}</td>
                <td>{{ bike.mileage }}</td>
            </tr>
            </tbody>
        </table>
    </div>
    <new-bike-form></new-bike-form>
</template>
<script lang="ts">
import {defineComponent} from 'vue';
import NewBikeForm from "@/components/newBikeForm.vue";
import {Bike, BikeResponse} from "@/models/bikes/bike";
import {getWithBody} from "@/models/genericFetch";

export default defineComponent({
    name: 'Bikes',
    emits: ['fetchUsername'],
    props: ["user"],
    components: {
        NewBikeForm,
    },
    data: function () {
        return {
            bikes: [] as Array<Bike>,
        }
    },
    created: async function () {
        await this.getBikes();
    },
    methods: {
        getBikes: async function () {
            let result = await getWithBody<BikeResponse>("/bike");
            if (result.status == 200) {
                this.bikes = result.body.bikes;
            }
        }
    }
})
</script>
