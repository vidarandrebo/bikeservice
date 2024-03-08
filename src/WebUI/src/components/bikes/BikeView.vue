<template>
    <article class="bg-gray-300 p-2 rounded">
        <table>
            <thead>
                <tr>
                    <th scope="row" colspan="2">
                        <h2>{{ bike.manufacturer }} {{ bike.model }}</h2>
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <th scope="row">
                        <p>Distance:</p>
                    </th>
                    <td>
                        <p>{{ bike.mileage }} km</p>
                    </td>
                </tr>
                <tr>
                    <th scope="row">
                        <p>Guid</p>
                    </th>
                    <td>
                        <p>{{ bike.id }}</p>
                    </td>
                </tr>
                <tr>
                    <th scope="row">
                        <p>Type</p>
                    </th>
                    <td>
                        <p>{{ equipmentType.name }}</p>
                    </td>
                </tr>
                <tr>
                    <th scope="row">
                        <p>Date</p>
                    </th>
                    <td>
                        <p>{{ bike.date }}</p>
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="space-x-1">
            <ButtonPrimary v-show="showEditButton" @click="showEdit">Edit</ButtonPrimary>
            <ButtonSecondary @click="deleteBike">Delete</ButtonSecondary>
        </div>
        <EditBikeForm
            v-show="showEditForm"
            :equipment-types="equipmentTypes"
            :bike="bike"
            @edit-done-event="editDoneHandler"
            @update-bikes-event="updateBikesHandler"
        ></EditBikeForm>
    </article>
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
