<template>
    <div class="card">
        <form id="edit-part" method="POST" v-on:submit.prevent="putPart">
            <div class="form-field">
                <label for="manufacturer">Manufacturer</label>
                <input type="text" id="manufacturer" v-model="partData.manufacturer" required>
            </div>
            <div class="form-field">
                <label for="model">Model</label>
                <input type="text" id="model" v-model="partData.model" required>
            </div>
            <div class="form-field">
                <label for="mileage">Mileage</label>
                <input type="number" id="mileage" v-model="partData.mileage" required>
            </div>
            <div class="form-field">
                <label for="type">Type</label>
                <select id="type" v-model="partData.typeId" required>
                    <option value="0">No Type</option>
                    <option v-for="partType in partTypes" :value="partType.id" :key="partType.id">{{
                            partType.name
                        }}
                    </option>
                </select>
            </div>
            <div class="form-field">
                <label for="type">Bike</label>
                <select id="type" v-model="partData.bikeId" required>
                    <option value="0">No Bike</option>
                    <option v-for="bike in bikes" :value="bike.id" :key="bike.id">{{
                            bike.manufacturer
                        }}
                        {{ bike.model }}
                    </option>
                </select>
            </div>
            <div class="form-field">
                <input type="submit" value="Save">
                <button v-on:click.prevent="hideForm">Cancel</button>
            </div>
        </form>
    </div>
</template>

<script lang="ts">

import {defineComponent} from "vue";
import {Part} from "../../models/parts/part.ts";
import {EquipmentType} from "../../models/equipmentTypes/equipmentType.ts";
import {Bike} from "../../models/bikes/bike.ts";
import {Category} from "../../models/equipmentTypes/category.ts";

export default defineComponent({
    name: "EditPartForm",
    data: function () {
        return {
            partData: new Part(),
        }
    },
    props: {
        equipmentTypes: {
            required: true,
            type: Array<EquipmentType>
        },
        part: {
            required: true,
            type: Part,
        },
        bikes: {
            required: true,
            type: Array<Bike>
        }
    },
    emits: {
        updatePartsEvent() {
            return true
        },
        editDoneEvent() {
            return true
        }
    },
    computed: {
        partTypes(): Array<EquipmentType> {
            return this.equipmentTypes.filter(t => t.category == Category.Part);
        }
    },
    created: function () {
        this.setFormDataFromProp();
    },
    watch: {
        part: function () {
            this.setFormDataFromProp();
        },
    },
    methods: {
        putPart: async function () {
            let result = await this.partData.putPartRequest();
            if (result.status === 200) {
                this.partData.clear();
                this.$emit('updatePartsEvent');
                this.$emit('editDoneEvent');
            }
        },
        hideForm: function () {
            this.$emit('editDoneEvent');
        },
        setFormDataFromProp() {
            this.partData = new Part(this.part);
        }
    },

})
</script>

<style scoped>
</style>
