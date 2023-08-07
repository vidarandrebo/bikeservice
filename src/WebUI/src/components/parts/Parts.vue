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

import {defineComponent} from "vue";
import PartView from "./PartView.vue";
import NewPartForm from "./NewPartForm.vue";
import {getPartsRequest, Part} from "../../models/parts/part.ts";
import {Bike, getBikesRequest} from "../../models/bikes/bike.ts";
import {EquipmentType, getTypeRequest} from "../../models/equipmentTypes/equipmentType.ts";

export default defineComponent({
    name: 'Parts',
    components: {PartView, NewPartForm},
    props: {
        user: {
            type: String,
        }
    },
    emits: {
        updateUsernameEvent() {
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
