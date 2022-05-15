<template>
    <div class="post">
        <table>
            <thead>
            <tr>
                <th>
                    Manufacturer
                </th>
                <th>
                    Partname
                </th>
                <th>
                    Km
                </th>
            </tr>
            </thead>
            <tbody>
            <tr>
                <td>
                    Trek
                </td>
                <td>
                    Madone 3.1
                </td>
                <td>
                    34500
                </td>
            </tr>
            </tbody>
        </table>
    </div>
    <new-bike-form></new-bike-form>
</template>
<script lang="ts">
import {defineComponent} from 'vue';
import NewBikeForm from "@/components/newBikeForm.vue";

export default defineComponent({
    name: 'Bikes',
    components: {
        NewBikeForm,
    },
    data: function () {
        return {
            manufacturer: "hei"
        }
    },
})
</script>
