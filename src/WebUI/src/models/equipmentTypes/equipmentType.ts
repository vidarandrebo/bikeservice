import { DataArrayResponse, FetchResponse, httpGetWithBody, httpPost } from "../httpMethods.ts";
import { Category } from "./category.ts";

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

    async addTypeRequest(): Promise<FetchResponse<null>> {
        return await httpPost<EquipmentType>("/api/type", this);
    }

    clear() {
        this.name = "";
        this.category = Category.Bike;
        this.id = "";
    }
}

/**
 * Acquires all equipment-types and returns an array of object containing them.
 */
export async function getTypeRequest(): Promise<EquipmentType[]> {
    const result = await httpGetWithBody<DataArrayResponse<EquipmentType>>("/api/type");
    if (result.status === 200) {
        return result.body.data.map(createType);
    }
    return [];
}

/**
 * Mapper for the types received from api-call
 * @param type The received object
 */
function createType(type: EquipmentType) {
    return new EquipmentType(type);
}
