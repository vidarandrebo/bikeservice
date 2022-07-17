import {DataArrayResponse, FetchResponse, getOrigin, httpDelete, httpGetWithBody, httpPost} from "@/models/httpMethods";
import {EquipmentType} from "@/models/equipmentTypes/equipmentType";

export interface IBike {
    id: string;
    manufacturer: string;
    model: string;
    mileage: number;
    typeId: string;

    addBikeRequest(): Promise<FetchResponse<null>>;

    deleteBikeRequest(): Promise<FetchResponse<null>>;

    clear(): void;
}

export class Bike implements IBike {
    id: string;
    manufacturer: string;
    model: string;
    mileage: number;
    typeId: string;


    async addBikeRequest(): Promise<FetchResponse<null>> {
        return await httpPost<IBike>("/api/bike", this);
    }

    async deleteBikeRequest(): Promise<FetchResponse<null>> {
        return await httpDelete("/api/bike", this.id);
    }

    clear(): void {
        this.manufacturer = "";
        this.model = "";
        this.mileage = 0.0;
        this.typeId = "";
    }

    // Copies over the fields in the argument object if given
    constructor(...args: IBike[]) {
        this.id = "";
        this.manufacturer = "";
        this.model = "";
        this.mileage = 0.0;
        this.typeId = "";
        if (args.length === 1) {
            Object.assign(this, args[0]);
        }
    }
}
export async function getBikesRequest(): Promise<IBike[]> {
    let result = await httpGetWithBody<DataArrayResponse<Bike>>("/api/bike");
    if (result.status === 200) {
        return result.body.data.map(createBike);
    }
    return [];
}


function createBike(bike: IBike) {
    return new Bike(bike);
}
