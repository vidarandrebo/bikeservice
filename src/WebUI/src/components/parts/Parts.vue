<template>
    <new-part-form @updatePartsEvent="updatePartsHandler" v-bind:equipment-types="equipmentTypes"></new-part-form>
    <part-view v-for="part in parts" v-bind:part="part" v-bind:bike="bike(part)"
               v-bind:equipment-type="equipmentType(part)" :key="part.id"
               @updatePartsEvent="updatePartsHandler"></part-view>
</template>
<script lang="ts">
import {defineComponent, PropType} from 'vue';
import NewPartForm from "@/components/parts/NewPartForm.vue";
import {getPartsRequest, IPart} from "@/models/parts/part";
import PartView from "@/components/parts/PartView.vue";
import {EquipmentType, getTypeRequest, IEquipmentType} from "@/models/equipmentTypes/equipmentType";
import {Bike, getBikesRequest, IBike} from "@/models/bikes/bike";

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
            bikes: [] as Array<IBike>,
            equipmentTypes: [] as Array<IEquipmentType>,
        }
    },
    created: async function () {
        this.parts = await getPartsRequest();
        this.equipmentTypes = await getTypeRequest();
        this.bikes = await getBikesRequest();
    },
    methods: {
        bike: function (part: IPart): IBike {
            let bike = this.bikes.find(b => b.id == part.bikeId);
            if (bike != undefined) {
                return bike;
            }
            return new Bike();
        },
        equipmentType: function (part: IPart): IEquipmentType {
            let type = this.equipmentTypes.find(t => t.id == part.typeId);
            if (type != undefined) {
                return type;
            }
            return new EquipmentType();
        },
        /**
         * Handler for updatePartsEvent
         */
        updatePartsHandler: async function () {
            this.parts = await getPartsRequest();
        }
    }
})
</script>
