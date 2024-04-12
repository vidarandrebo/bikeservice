<template>
    <main>
        <article class="flex max-w-prose flex-col">
            <h1 class="">Parts</h1>
            <NewPartForm :bikes="bikes" :equipment-types="equipmentTypes" @update-parts-event="updatePartsHandler">
            </NewPartForm>
            <ol class="space-y-2">
                <li v-for="part in parts" :key="part.id">
                    <PartView
                        :bikes="bikes"
                        :equipment-types="equipmentTypes"
                        :part="part"
                        @update-parts-event="updatePartsHandler"
                    >
                    </PartView>
                </li>
            </ol>
        </article>
    </main>
</template>
<script lang="ts">
import { defineComponent } from "vue";
import PartView from "./PartView.vue";
import NewPartForm from "./NewPartForm.vue";
import { getPartsRequest, Part } from "../../Models/Parts/Part.ts";
import { Bike, getBikesRequest } from "../../Models/Bikes/Bike.ts";
import { EquipmentType, getTypeRequest } from "../../Models/EquipmentTypes/EquipmentType.ts";

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
