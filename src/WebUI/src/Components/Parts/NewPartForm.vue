<template>
    <article>
        <FormField>
            <ButtonPrimary v-show="!show" @click="showForm">New Part</ButtonPrimary>
        </FormField>
        <form v-show="show" id="new-part" method="POST" @submit.prevent="addPart">
            <FormField>
                <LabelPrimary for="manufacturer">Manufacturer</LabelPrimary>
                <InputText id="manufacturer" v-model="partData.manufacturer" required />
            </FormField>
            <FormField>
                <LabelPrimary for="model">Model</LabelPrimary>
                <InputText id="model" v-model="partData.model" required />
            </FormField>
            <FormField>
                <LabelPrimary for="mileage">Mileage</LabelPrimary>
                <InputNumber id="mileage" v-model="partData.mileage" required />
            </FormField>
            <FormField>
                <LabelPrimary for="type">Type</LabelPrimary>
                <SelectPrimary id="type" v-model="partData.typeId" required>
                    <option value="0">No Type</option>
                    <option v-for="partType in partTypes" :key="partType.id" :value="partType.id">
                        {{ partType.name }}
                    </option>
                </SelectPrimary>
            </FormField>
            <FormField>
                <LabelPrimary for="type">Bike</LabelPrimary>
                <SelectPrimary id="type" v-model="partData.bikeId" required>
                    <option value="0">No Bike</option>
                    <option v-for="bike in bikes" :key="bike.id" :value="bike.id">
                        {{ bike.manufacturer }}
                        {{ bike.model }}
                    </option>
                </SelectPrimary>
            </FormField>
            <FormField class="space-x-1">
                <ButtonPrimary type="submit">Add</ButtonPrimary>
                <ButtonSecondary @click="hideForm">Cancel</ButtonSecondary>
            </FormField>
        </form>
    </article>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { Part } from "../../Models/Parts/Part.ts";
import { EquipmentType } from "../../Models/EquipmentTypes/EquipmentType.ts";
import { Bike } from "../../Models/Bikes/Bike.ts";
import { Category } from "../../Models/EquipmentTypes/Category.ts";
import ButtonPrimary from "../Common/ButtonPrimary.vue";
import LabelPrimary from "../Common/LabelPrimary.vue";
import InputText from "../Common/InputText.vue";
import InputNumber from "../Common/InputNumber.vue";
import SelectPrimary from "../Common/SelectPrimary.vue";
import ButtonSecondary from "../Common/ButtonSecondary.vue";
import FormField from "../Common/FormField.vue";

export default defineComponent({
    name: "NewPartForm",
    components: { FormField, ButtonSecondary, SelectPrimary, InputNumber, InputText, LabelPrimary, ButtonPrimary },
    props: {
        equipmentTypes: {
            required: true,
            type: Array<EquipmentType>
        },
        bikes: {
            required: true,
            type: Array<Bike>
        }
    },
    emits: {
        updatePartsEvent() {
            return true;
        }
    },
    data: function () {
        return {
            partData: new Part(),
            show: false as boolean
        };
    },
    computed: {
        partTypes(): Array<EquipmentType> {
            return this.equipmentTypes.filter((t) => t.category == Category.Part);
        }
    },
    methods: {
        addPart: async function () {
            let result = await this.partData.addPartRequest();
            if (result.status == 201) {
                this.partData.clear();
                this.$emit("updatePartsEvent");
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
