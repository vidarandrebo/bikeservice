<template>
    <div class="card">
        <h5>{{ part.manufacturer }} {{ part.model }} {{ equipmentType.name }}</h5>
        <details>
            <summary>More</summary>
            <div class="part-specs">
                <div class="spec">
                    <p>Distance:</p>
                    <p>{{ part.mileage }} km</p>
                </div>
                <div class="spec">
                    <p>Guid:</p>
                    <p>{{ part.id }}</p>
                </div>
                <div class="spec">
                    <p>Bike:</p>
                    <p>{{ bike.manufacturer }} {{ bike.model }}</p>
                </div>
            </div>
            <button v-on:click="deletePart">Delete</button>
        </details>
    </div>
</template>

<script lang="ts">
import {defineComponent, PropType} from "vue";
import {IPart} from "@/models/parts/part";
import {IBike} from "@/models/bikes/bike";
import {IEquipmentType} from "@/models/equipmentTypes/equipmentType";

export default defineComponent({
    name: "PartView",
    props: {
        part: {
            type: Object as PropType<IPart>
        },
        bike: {
            type: Object as PropType<IBike>
        },
        equipmentType: {
            type: Object as PropType<IEquipmentType>
        }
    },
    methods: {
        deletePart: async function () {
            if (this.part != null) {
                await this.part.deletePartRequest();
                this.$emit('updatePartsEvent');
            }
        }
    },
    emits: {
        updatePartsEvent() {
            return true
        },
    }
})
</script>


<style scoped>
.part-specs {
    display: flex;
    flex-wrap: wrap;
}

.part-specs .spec {
    margin-right: 1rem;
}

.spec p {
    display: inline;
    margin-right: 1rem;
}

.part-view {
    background-color: var(--card-color);
    margin: 0.5rem;
    padding: 1rem;
}
</style>
