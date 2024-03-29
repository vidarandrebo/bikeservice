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
            <button @click="deletePart">Delete</button>
            <button v-show="showEditButton" @click="showEdit">Edit</button>
            <edit-part-form
                v-show="showEditForm"
                :equipment-types="equipmentTypes"
                :bikes="bikes"
                :part="part"
                @edit-done-event="editDoneHandler"
                @update-parts-event="updatePartsHandler"
            >
            </edit-part-form>
        </details>
    </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import EditPartForm from "./EditPartForm.vue";
import { Part } from "../../models/parts/part.ts";
import { Bike } from "../../models/bikes/bike.ts";
import { EquipmentType } from "../../models/equipmentTypes/equipmentType.ts";

export default defineComponent({
    name: "PartView",
    components: { EditPartForm },
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
