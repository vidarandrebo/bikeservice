<template>
    <form id="new-bike" method="POST" @submit.prevent="putBike">
        <FormField>
            <LabelPrimary for="manufacturer">Manufacturer</LabelPrimary>
            <InputText id="manufacturer" v-model="bikeData.manufacturer" required />
        </FormField>
        <FormField>
            <LabelPrimary for="model">Model</LabelPrimary>
            <InputText id="model" v-model="bikeData.model" required />
        </FormField>
        <FormField>
            <LabelPrimary for="mileage">Mileage</LabelPrimary>
            <InputNumber id="mileage" v-model="bikeData.mileage" required />
        </FormField>
        <FormField>
            <LabelPrimary for="date">Date</LabelPrimary>
            <InputDate id="date" v-model="date" required />
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
        <FormField>
            <ButtonPrimary type="submit">Save</ButtonPrimary>
            <ButtonSecondary @click.prevent="hideForm">Cancel</ButtonSecondary>
        </FormField>
    </form>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { Bike } from "../../Models/Bikes/Bike.ts";
import { EquipmentType } from "../../Models/EquipmentTypes/EquipmentType.ts";
import { Category } from "../../Models/EquipmentTypes/Category.ts";
import { getDateString } from "../../Models/DateFormatter.ts";
import ButtonPrimary from "../Common/ButtonPrimary.vue";
import ButtonSecondary from "../Common/ButtonSecondary.vue";
import LabelPrimary from "../Common/LabelPrimary.vue";
import SelectPrimary from "../Common/SelectPrimary.vue";
import InputText from "../Common/InputText.vue";
import InputNumber from "../Common/InputNumber.vue";
import InputDate from "../Common/InputDate.vue";
import FormField from "../Common/FormField.vue";

export default defineComponent({
    name: "EditBikeForm",
    components: {
        FormField,
        ButtonSecondary,
        InputDate,
        InputNumber,
        InputText,
        SelectPrimary,
        LabelPrimary,
        ButtonPrimary
    },
    props: {
        equipmentTypes: {
            required: true,
            type: Array<EquipmentType>
        },
        bike: {
            required: true,
            type: Bike
        }
    },
    emits: {
        updateBikesEvent() {
            return true;
        },
        editDoneEvent() {
            return true;
        }
    },
    data: function () {
        return {
            bikeData: new Bike(),
            date: ""
        };
    },
    computed: {
        bikeTypes(): Array<EquipmentType> {
            return this.equipmentTypes.filter((t) => t.category == Category.Bike);
        }
    },
    watch: {
        bike: function () {
            this.setFormDataFromProp();
        }
    },
    created: function () {
        this.setFormDataFromProp();
    },
    methods: {
        putBike: async function () {
            this.bikeData.date = new Date(this.date);
            let result = await this.bikeData.putBikeRequest();
            if (result.status === 200) {
                this.date = "";
                this.bikeData.clear();
                this.$emit("updateBikesEvent");
                this.$emit("editDoneEvent");
            }
        },
        hideForm: function () {
            this.$emit("editDoneEvent");
        },
        setFormDataFromProp() {
            this.bikeData = new Bike(this.bike);
            this.date = getDateString(this.bike.date);
        }
    }
});
</script>
