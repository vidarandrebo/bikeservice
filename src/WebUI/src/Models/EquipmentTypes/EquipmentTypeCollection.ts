import { ref, Ref } from "vue";
import { EquipmentType } from "./EquipmentType.ts";

export type EquipmentTypeCollection = {
    equipmentTypes: Ref<EquipmentType[]>;
    fetchEquipmentTypes: () => void;
};

export function DefaultEquipmentTypeCollection(): EquipmentTypeCollection {
    return {
        equipmentTypes: ref<EquipmentType[]>([]),
        fetchEquipmentTypes: () => {}
    };
}
