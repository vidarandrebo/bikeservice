<template>
    <div class="new-part-view">
        <button v-on:click="showForm" v-show="!show">New Part</button>
        <form id="new-part" method="POST" v-on:submit.prevent="addPart" v-show="show">
            <label for="manufacturer">Manufacturer</label>
            <input type="text" id="manufacturer" v-model="partData.manufacturer" required>
            <label for="model">Model</label>
            <input type="text" id="model" v-model="partData.model" required>
            <label for="mileage">Mileage</label>
            <input type="number" id="mileage" v-model="partData.mileage" required>
            <input type="submit" value="Add">
            <button v-on:click="hideForm">Cancel</button>
        </form>
    </div>
</template>

<script lang="ts">
import {defineComponent} from "vue";
import {Part} from "@/models/parts/part";

export default defineComponent({
    name: "NewPartForm",
    data: function () {
        return {
            partData: new Part(),
            show: false as boolean,
        }
    },
    emits: {
        updatePartsEvent() {
            return true
        }
    },
    methods: {
        addPart: async function () {
            let result = await this.partData.addPartRequest();
            if (result.status == 201) {
                this.partData.clear();
                this.$emit('updatePartsEvent');
            }
        },
        hideForm: function () {
            this.show = false;
        },
        showForm: function () {
            this.show = true;
        }
    }

})
</script>

<style scoped>
.new-part-view {
    background-color: beige;
    margin: 0.5rem;
    padding: 1rem;
}

</style>