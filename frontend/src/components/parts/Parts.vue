<template>
    <new-part-form @updatePartsEvent="updatePartsHandler" v-bind:equipment-types="equipmentTypes"></new-part-form>
    <part-view v-for="part in parts" v-bind:part="part" @updatePartsEvent="updatePartsHandler"></part-view>
</template>
<script lang="ts">
import {defineComponent, PropType} from 'vue';
import NewPartForm from "@/components/parts/NewPartForm.vue";
import {getPartsRequest, IPart} from "@/models/parts/part";
import PartView from "@/components/parts/PartView.vue";
import {getTypeRequest, IEquipmentType} from "@/models/equipmentTypes/equipmentType";

export default defineComponent({
    name: 'Parts',
    components: {PartView, NewPartForm},
    props: {
        user: {
            type: String as PropType<string>,
        }
    },
    emits: {
        updateUsernameEvent(value: string) {
            return true
        },
    },
    data: function () {
        return {
            parts: [] as Array<IPart>,
            equipmentTypes: [] as Array<IEquipmentType>,
        }
    },
    created: async function () {
        await this.getParts();
        await this.getTypes();
    },
    methods: {
        getParts: async function () {
            this.parts = await getPartsRequest();
        },
        getTypes: async function () {
            this.equipmentTypes = await getTypeRequest();
        },
        /**
         * Handler for updatePartsEvent
         */
        updatePartsHandler: async function () {
            await this.getParts();
        }
    }
})
</script>
