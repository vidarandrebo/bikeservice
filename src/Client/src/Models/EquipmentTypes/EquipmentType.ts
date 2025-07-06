import { Category } from "./Category.ts";
import { getTypeApi } from "../Api.ts";
import { EquipmentTypeResponse } from "../../Gen";

export class EquipmentType {
    id: string;
    name: string;
    category: Category;

    constructor(...args: EquipmentType[]) {
        this.id = "";
        this.name = "";
        this.category = Category.Bike;
        if (args.length === 1) {
            Object.assign(this, args[0]);
        }
    }

    async addTypeRequest(): Promise<number> {
        const client = getTypeApi();

        try {
            await client.apiTypePost({ postEquipmentTypeRequest: this });
            return 201;
        } catch {
            console.log("failed to add types");
        }
        return -1;
    }

    clear() {
        this.name = "";
        this.category = Category.Bike;
        this.id = "";
    }
    static fromResponse(response: EquipmentTypeResponse): EquipmentType {
        const t = new EquipmentType();
        t.id = response.id;
        t.name = response.name;
        t.category = response.category;
        return t;
    }
}

/**
 * Acquires all equipment-types and returns an array of object containing them.
 */
export async function getTypeRequest(): Promise<EquipmentType[]> {
    const client = getTypeApi();
    try {
        const response = await client.apiTypeGet();
        return response.map(EquipmentType.fromResponse);
    } catch {
        console.log("failed to get types");
    }
    return [];
}
