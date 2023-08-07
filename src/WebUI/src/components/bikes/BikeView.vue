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
            <button v-on:click="deleteBike">Delete</button>
            <button v-on:click="showEdit" v-show="showEditButton">Edit</button>
            <edit-bike-form v-show="showEditForm" @editDoneEvent="editDoneHandler" @updateBikesEvent="updateBikesHandler"
                            v-bind:equipment-types="equipmentTypes" v-bind:bike="bike"></edit-bike-form>
        </details>
    </div>
</template>

<script lang="ts">
import {IBike} from "@/models/bikes/bike";
import {defineComponent, PropType} from "vue";
import {EquipmentType, IEquipmentType} from "@/models/equipmentTypes/equipmentType";
import EditBikeForm from "@/components/bikes/EditBikeForm.vue";

export default defineComponent({
    name: "BikeView",
    components: {
        EditBikeForm,
    },
    props: {
        bike: {
            type: Object as PropType<IBike>
        },
        equipmentTypes: {
            required: true,
            type: Array<IEquipmentType>,
        }
    },
    data: function () {
        return {
            showEditForm: false,
            showEditButton: true,
        }
    },
    methods: {
        deleteBike: async function () {
            if (this.bike != null) {
                await this.bike.deleteBikeRequest();
                this.$emit('updateBikesEvent');
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
        updateBikesHandler: function() {
            this.$emit("updateBikesEvent");
        }
    },
    computed: {
        equipmentType(): IEquipmentType {
            let type = this.equipmentTypes.find(t => t.id == this.bike.typeId);
            if (type != undefined) {
                return type;
            }
            return new EquipmentType();
        }
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

</style>