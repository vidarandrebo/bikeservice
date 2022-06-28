import {FetchResponse, httpDelete, httpPost} from "@/models/httpMethods";

export interface IBike {
    id: string;
    manufacturer: string;
    model: string;
    mileage: number;

    addBikeRequest(): Promise<FetchResponse<null>>;

    deleteBikeRequest(): Promise<FetchResponse<null>>;

    clear(): void;
}

export class Bike implements IBike {
    id: string;
    manufacturer: string;
    model: string;
    mileage: number;

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
    }

    // Copies over the fields in the argument object if given
    constructor(...args: IBike[]) {
        this.id = "";
        this.manufacturer = "";
        this.model = "";
        this.mileage = 0.0;
        if (args.length === 1) {
            Object.assign(this, args[0]);
        }
    }
}


export function createBike(bike: IBike) {
    return new Bike(bike);
}
