<template>
    <new-part-form @updatePartsEvent="updatePartsHandler"></new-part-form>
    <part-view v-for="part in parts" v-bind:part="part" @updatePartsEvent="updatePartsHandler"></part-view>
</template>
<script lang="ts">
import {defineComponent, PropType} from 'vue';
import NewPartForm from "@/components/parts/NewPartForm.vue";
import {createPart, IPart, Part} from "@/models/parts/part";
import {DataArrayResponse, getHost, httpGetWithBody} from "@/models/httpMethods";
import PartView from "@/components/parts/PartView.vue";

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
        }
    },
    created: async function () {
        await this.getParts();
    },
    methods: {
        getParts: async function () {
            let result = await httpGetWithBody<DataArrayResponse<Part>>("/api/part");
            if (result.status === 200) {
                this.parts = result.body.data.map(createPart);
            }
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
