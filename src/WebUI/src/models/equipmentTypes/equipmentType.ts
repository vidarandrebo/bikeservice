import {Category} from "@/models/equipmentTypes/category";
import {DataArrayResponse, FetchResponse, httpGetWithBody, httpPost} from "@/models/httpMethods";

export interface IEquipmentType {
    id: string;
    name: string;
    category: Category;

    addTypeRequest(): Promise<FetchResponse<null>>;

    clear(): void;
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

    async addTypeRequest(): Promise<FetchResponse<null>> {
        return await httpPost<IEquipmentType>("/api/type", this);
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
export async function getTypeRequest(): Promise<IEquipmentType[]> {
    let result = await httpGetWithBody<DataArrayResponse<IEquipmentType>>("/api/type");
    if (result.status === 200) {
        return result.body.data.map(createType);
    }
    return [];
}

/**
 * Mapper for the types received from api-call
 * @param type The received object
 */
function createType(type: IEquipmentType) {
    return new EquipmentType(type);
}