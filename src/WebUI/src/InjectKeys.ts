import { InjectionKey } from "vue";
import { Bike } from "./Models/Bikes/Bike.ts";
import { EquipmentType } from "./Models/EquipmentTypes/EquipmentType.ts";

export const BikesKey: InjectionKey<Bike[]> = Symbol("BikesKey");
export const EquipmentTypesKey: InjectionKey<EquipmentType[]> = Symbol("EquipmentTypesKey");
