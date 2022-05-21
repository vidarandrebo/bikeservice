import {Category} from "@/models/equipmentTypes/category";

export interface IEquipmentType {
    id: string;
    name: string;
    category: Category;
}

export class EquipmentType implements IEquipmentType {
    id: string;
    name: string;
    category: Category;

    constructor(...args: IEquipmentType[]) {
        this.id = "";
        this.name = "";
        this.category = Category.Bike;
        if (args.length === 1) {
            Object.assign(this, args[0]);
        }
    }
}