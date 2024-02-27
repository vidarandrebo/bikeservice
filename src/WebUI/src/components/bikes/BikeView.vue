<template>
    <div>
        <h5>{{ bike.manufacturer }} {{ bike.model }}</h5>
        <details>
            <summary>More</summary>
            <div>
                <div>
                    <p>Distance:</p>
                    <p>{{ bike.mileage }} km</p>
                </div>
                <div>
                    <p>Guid:</p>
                    <p>{{ bike.id }}</p>
                </div>
            </div>
            <div>
                <p>Type</p>
                <p>{{ equipmentType.name }}</p>
            </div>
            <div>
                <p>Date</p>
                <p>{{ bike.date }}</p>
            </div>
            <ButtonPrimary v-show="showEditButton" @click="showEdit">Edit</ButtonPrimary>
            <ButtonSecondary @click="deleteBike">Delete</ButtonSecondary>
            <EditBikeForm
                v-show="showEditForm"
                :equipment-types="equipmentTypes"
                :bike="bike"
                @edit-done-event="editDoneHandler"
                @update-bikes-event="updateBikesHandler"
            ></EditBikeForm>
        </details>
    </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import EditBikeForm from "./EditBikeForm.vue";
import { Bike } from "../../models/bikes/bike.ts";
import { EquipmentType } from "../../models/equipmentTypes/equipmentType.ts";
import ButtonPrimary from "../common/ButtonPrimary.vue";
import ButtonSecondary from "../common/ButtonSecondary.vue";

export default defineComponent({
    name: "BikeView",
    components: {
        ButtonSecondary,
        EditBikeForm,
        ButtonPrimary
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
