<template>
    <new-part-form @updatePartsEvent="updatePartsHandler"
                   v-bind:equipment-types="equipmentTypes"
                   v-bind:bikes="bikes">
    </new-part-form>
    <part-view v-for="part in parts" :key="part.id"
               v-bind:part="part"
               v-bind:equipment-types="equipmentTypes"
               v-bind:bikes="bikes"
               @updatePartsEvent="updatePartsHandler">
    </part-view>
</template>
<script lang="ts">
import {defineComponent, PropType} from 'vue';
import NewPartForm from "@/components/parts/NewPartForm.vue";
import {getPartsRequest, Part} from "@/models/parts/part";
import PartView from "@/components/parts/PartView.vue";
import {EquipmentType, getTypeRequest} from "@/models/equipmentTypes/equipmentType";
import {Bike, getBikesRequest} from "@/models/bikes/bike";

export default defineComponent({
    name: 'Parts',
    components: {PartView, NewPartForm},
    props: {
        user: {
            type: String,
        }
    },
    emits: {
        updateUsernameEvent(value: string) {
            return true
        },
    },
    data: function () {
        return {
            parts: [] as Array<Part>,
            bikes: [] as Array<Bike>,
            equipmentTypes: [] as Array<EquipmentType>,
        }
    },
    created: async function () {
        const partsPromise = getPartsRequest();
        const bikesPromise = getBikesRequest();
        const equipmentTypesPromise = getTypeRequest();
        this.parts = await partsPromise;
        this.bikes = await bikesPromise;
        this.equipmentTypes = await equipmentTypesPromise;
    },
    methods: {
        /**
         * Handler for updatePartsEvent
         */
        updatePartsHandler: async function () {
            this.parts = await getPartsRequest();
        }
    }
})
</script>
