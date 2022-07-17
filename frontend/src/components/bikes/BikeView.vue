<template>
    <div class="bike-view">
        <h5>{{ bike.manufacturer }} {{ bike.model }}</h5>
        <details>
            <summary>More</summary>
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
            <div class="spec">
                <p>Type</p>
                <p>{{ type }}</p>
            </div>
            <button v-on:click="deleteBike">Delete</button>
        </details>
    </div>
</template>

<script lang="ts">
import {IBike} from "@/models/bikes/bike";
import {defineComponent, PropType} from "vue";
import {EquipmentType} from "@/models/equipmentTypes/equipmentType";

export default defineComponent({
    name: "BikeView",
    props: {
        bike: {
            type: Object as PropType<IBike>
        },
        equipmentTypes: {
            type: Array<EquipmentType>
        }
    },
    computed: {
        type() : string{
            let result = this.equipmentTypes?.find(p => p.id == this.bike?.typeId);
            if (result != undefined) {
                return result.name;
            }
            return "Type not set";
        }
    },
    methods: {
        deleteBike: async function () {
            if (this.bike != null) {
                await this.bike.deleteBikeRequest();
                this.$emit('updateBikesEvent');
            }
        },
    },
    emits: {
        updateBikesEvent() {
            return true
        },
    }
})
</script>


<style scoped>
.bike-specs {
    display: flex;
    flex-wrap: wrap;
}

.bike-specs .spec {
    margin-right: 1rem;
}

.spec p {
    display: inline;
    margin-right: 1rem;
}

.bike-view {
    background-color: beige;
    margin: 0.5rem;
    padding: 1rem;
}
</style>