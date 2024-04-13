<template>
    <article class="bg-gray-300 p-2 rounded">
        <table>
            <thead>
                <tr>
                    <th colspan="2" scope="row">
                        <HeadingH2>{{ part.manufacturer }} {{ part.model }}</HeadingH2>
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <th scope="row">
                        <p>Distance:</p>
                    </th>
                    <td>
                        <p>{{ part.mileage }} km</p>
                    </td>
                </tr>
                <tr>
                    <th scope="row">
                        <p>Guid</p>
                    </th>
                    <td>
                        <p>{{ part.id }}</p>
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
                        <p>Bike</p>
                    </th>
                    <td>
                        <p>{{ bike.manufacturer }} {{ bike.model }}</p>
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="space-x-1">
            <ButtonPrimary v-show="showEditButton" @click="showEdit">Edit</ButtonPrimary>
            <ButtonSecondary v-show="showEditButton" @click="deletePart">Delete</ButtonSecondary>
        </div>
        <EditPartForm
            v-show="showEditForm"
            :bikes="bikes"
            :equipment-types="equipmentTypes"
            :part="part"
            @edit-done-event="editDoneHandler"
            @update-parts-event="updatePartsHandler"
        >
        </EditPartForm>
    </article>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import EditPartForm from "./EditPartForm.vue";
import { Part } from "../../Models/Parts/Part.ts";
import { Bike } from "../../Models/Bikes/Bike.ts";
import { EquipmentType } from "../../Models/EquipmentTypes/EquipmentType.ts";
import ButtonPrimary from "../Common/ButtonPrimary.vue";
import ButtonSecondary from "../Common/ButtonSecondary.vue";
import HeadingH2 from "../Common/Headings/HeadingH2.vue";

export default defineComponent({
    name: "PartView",
    components: { HeadingH2, ButtonSecondary, ButtonPrimary, EditPartForm },
    props: {
        part: {
            required: true,
            type: Part
        },
        bikes: {
            required: true,
            type: Array<Bike>
        },
        equipmentTypes: {
            required: true,
            type: Array<EquipmentType>
        }
    },
    emits: {
        updatePartsEvent() {
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
        bike(): Bike {
            let bike = this.bikes.find((b) => b.id == this.part.bikeId);
            if (bike != undefined) {
                return bike;
            }
            return new Bike();
        },
        equipmentType(): EquipmentType {
            let type = this.equipmentTypes.find((t) => t.id == this.part.typeId);
            if (type != undefined) {
                return type;
            }
            return new EquipmentType();
        }
    },
    methods: {
        deletePart: async function () {
            if (this.part != null) {
                await this.part.deletePartRequest();
                this.$emit("updatePartsEvent");
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
        updatePartsHandler: function () {
            this.$emit("updatePartsEvent");
        }
    }
});
</script>
