<template>
    <ButtonPrimary v-show="!show" @click="showForm">New Bike</ButtonPrimary>
    <form v-show="show" id="new-bike" method="POST" @submit.prevent="addBike">
        <div>
            <LabelPrimary for="manufacturer">Manufacturer</LabelPrimary>
            <TextInput id="manufacturer" v-model="bikeData.manufacturer" required />
        </div>
        <div>
            <LabelPrimary for="model">Model</LabelPrimary>
            <TextInput id="model" v-model="bikeData.model" required />
        </div>
        <div>
            <LabelPrimary for="mileage">Mileage</LabelPrimary>
            <NumberInput id="mileage" v-model="bikeData.mileage" required />
        </div>
        <div>
            <LabelPrimary for="date">Date</LabelPrimary>
            <DateInput id="date" v-model="date" required />
        </div>
        <div>
            <LabelPrimary for="type">Type</LabelPrimary>
            <SelectPrimary id="type" v-model="bikeData.typeId" required>
                <option value="0">No Type</option>
                <option v-for="bikeType in bikeTypes" :key="bikeType.id" :value="bikeType.id">
                    {{ bikeType.name }}
                </option>
            </SelectPrimary>
        </div>
        <div>
            <ButtonPrimary type="submit">Add</ButtonPrimary>
            <ButtonSecondary @click="hideForm">Cancel</ButtonSecondary>
        </div>
    </form>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { Bike } from "../../models/bikes/bike.ts";
import { EquipmentType } from "../../models/equipmentTypes/equipmentType.ts";
import { Category } from "../../models/equipmentTypes/category.ts";
import ButtonPrimary from "../common/ButtonPrimary.vue";
import LabelPrimary from "../common/LabelPrimary.vue";
import TextInput from "../common/InputText.vue";
import NumberInput from "../common/InputNumber.vue";
import DateInput from "../common/InputDate.vue";
import SelectPrimary from "../common/SelectPrimary.vue";
import ButtonSecondary from "../common/ButtonSecondary.vue";

export default defineComponent({
    name: "NewBikeForm",
    components: { ButtonSecondary, SelectPrimary, DateInput, NumberInput, TextInput, LabelPrimary, ButtonPrimary },
    props: {
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
            bikeData: new Bike(),
            date: "",
            show: false as boolean
        };
    },
    computed: {
        bikeTypes(): Array<EquipmentType> {
            return this.equipmentTypes.filter((t) => t.category == Category.Bike);
        }
    },
    methods: {
        addBike: async function () {
            this.bikeData.date = new Date(this.date);
            let result = await this.bikeData.addBikeRequest();
            if (result.status === 201) {
                this.date = "";
                this.bikeData.clear();
                this.hideForm();
                this.$emit("updateBikesEvent");
            }
        },
        hideForm: function () {
            this.show = false;
        },
        showForm: function () {
            this.show = true;
        }
    }
});
</script>
