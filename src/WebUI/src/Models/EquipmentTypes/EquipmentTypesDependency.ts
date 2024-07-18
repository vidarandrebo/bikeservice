import { ref, Ref } from "vue";
import { EquipmentType } from "./EquipmentType.ts";

export type EquipmentTypeDependency = {
    equipmentTypes: Ref<EquipmentType[]>;
    setEquipmentTypes: (value: EquipmentType[]) => void;
};

export function DefaultEquipmentTypeDependency(): EquipmentTypeDependency {
    return {
        equipmentTypes: ref<EquipmentType[]>([]),
        setEquipmentTypes: () => {}
    };
}
