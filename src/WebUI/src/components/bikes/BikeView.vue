<template>
    <div class="card">
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
                <p>{{ equipmentType.name }}</p>
            </div>
            <div class="spec">
                <p>Date</p>
                <p>{{ bike.date }}</p>
            </div>
            <button @click="deleteBike">Delete</button>
            <button v-show="showEditButton" @click="showEdit">Edit</button>
            <edit-bike-form
                v-show="showEditForm"
                :equipment-types="equipmentTypes"
                :bike="bike"
                @edit-done-event="editDoneHandler"
                @update-bikes-event="updateBikesHandler"
            ></edit-bike-form>
        </details>
    </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import EditBikeForm from "./EditBikeForm.vue";
import { Bike } from "../../models/bikes/bike.ts";
import { EquipmentType } from "../../models/equipmentTypes/equipmentType.ts";

export default defineComponent({
    name: "BikeView",
    components: {
        EditBikeForm
    },
    props: {
        bike: {
            required: true,
            type: Bike
        },
        equipmentTypes: {
            required: true,
            type: Array<EquipmentType>
        }
    },
    emits: {
        updateBikesEvent() {
            return true;
        }
    },
    data: function () {
        return {
            showEditForm: false,
            showEditButton: true
        };
    },
    computed: {
        equipmentType(): EquipmentType {
            let type = this.equipmentTypes.find((t) => t.id == this.bike.typeId);
            if (type != undefined) {
                return type;
            }
            return new EquipmentType();
        }
    },
    methods: {
        deleteBike: async function () {
            if (this.bike != null) {
                await this.bike.deleteBikeRequest();
                this.$emit("updateBikesEvent");
            }
        },
        showEdit: function () {
            this.showEditForm = true;
            this.showEditButton = false;
        },
        editDoneHandler: function () {
            this.showEditForm = false;
            this.showEditButton = true;
        },
        updateBikesHandler: function () {
            this.$emit("updateBikesEvent");
        }
    }
});
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
</style>
