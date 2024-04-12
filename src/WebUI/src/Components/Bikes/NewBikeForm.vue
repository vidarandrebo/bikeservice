<template>
    <article>
        <ButtonPrimary v-show="!show" @click="showForm">New Bike</ButtonPrimary>
        <form v-show="show" id="new-bike" method="POST" @submit.prevent="addBike">
            <FormField>
                <LabelPrimary for="manufacturer">Manufacturer</LabelPrimary>
                <TextInput id="manufacturer" v-model="bikeData.manufacturer" required />
            </FormField>
            <FormField>
                <LabelPrimary for="model">Model</LabelPrimary>
                <TextInput id="model" v-model="bikeData.model" required />
            </FormField>
            <FormField>
                <LabelPrimary for="mileage">Mileage</LabelPrimary>
                <NumberInput id="mileage" v-model="bikeData.mileage" required />
            </FormField>
            <FormField>
                <LabelPrimary for="date">Date</LabelPrimary>
                <DateInput id="date" v-model="date" required />
            </FormField>
            <FormField>
                <LabelPrimary for="type">Type</LabelPrimary>
                <SelectPrimary id="type" v-model="bikeData.typeId" required>
                    <option value="0">No Type</option>
                    <option v-for="bikeType in bikeTypes" :key="bikeType.id" :value="bikeType.id">
                        {{ bikeType.name }}
                    </option>
                </SelectPrimary>
            </FormField>
            <FormField class="flex space-x-2">
                <ButtonPrimary type="submit">Add</ButtonPrimary>
                <ButtonSecondary @click="hideForm">Cancel</ButtonSecondary>
            </FormField>
        </form>
    </article>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { Bike } from "../../Models/Bikes/Bike.ts";
import { EquipmentType } from "../../Models/EquipmentTypes/EquipmentType.ts";
import { Category } from "../../Models/EquipmentTypes/Category.ts";
import ButtonPrimary from "../Common/ButtonPrimary.vue";
import LabelPrimary from "../Common/LabelPrimary.vue";
import TextInput from "../Common/InputText.vue";
import NumberInput from "../Common/InputNumber.vue";
import DateInput from "../Common/InputDate.vue";
import SelectPrimary from "../Common/SelectPrimary.vue";
import ButtonSecondary from "../Common/ButtonSecondary.vue";
import FormField from "../Common/FormField.vue";

export default defineComponent({
    name: "NewBikeForm",
    components: {
        FormField,
        ButtonSecondary,
        SelectPrimary,
        DateInput,
        NumberInput,
        TextInput,
        LabelPrimary,
        ButtonPrimary
    },
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
