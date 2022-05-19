<template>
    <div class="content">
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
</template>
<script lang="ts">
import {defineComponent, PropType} from 'vue';

export default defineComponent({
    name: 'Parts',
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
            manufacturer: "hei"
        }
    },
})
</script>
