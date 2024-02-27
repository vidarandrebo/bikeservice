<template>
    <NewPartForm :equipment-types="equipmentTypes" :bikes="bikes" @update-parts-event="updatePartsHandler">
    </NewPartForm>
    <PartView
        v-for="part in parts"
        :key="part.id"
        :part="part"
        :equipment-types="equipmentTypes"
        :bikes="bikes"
        @update-parts-event="updatePartsHandler"
    >
    </PartView>
</template>
<script lang="ts">
import { defineComponent } from "vue";
import PartView from "./PartView.vue";
import NewPartForm from "./NewPartForm.vue";
import { getPartsRequest, Part } from "../../models/parts/part.ts";
import { Bike, getBikesRequest } from "../../models/bikes/bike.ts";
import { EquipmentType, getTypeRequest } from "../../models/equipmentTypes/equipmentType.ts";

export default defineComponent({
    name: "PartsPage",
    components: { PartView, NewPartForm },
    props: {
        user: {
            type: String,
            default: ""
        }
    },
    emits: {
        updateUsernameEvent() {
            return true;
        }
    },
    data: function () {
        return {
            parts: [] as Array<Part>,
            bikes: [] as Array<Bike>,
            equipmentTypes: [] as Array<EquipmentType>
        };
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
});
</script>
