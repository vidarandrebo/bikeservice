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
export class Bike implements IBike{
    id: string;
    manufacturer: string;
    model: string;
    mileage : number;
    async addBikeRequest(): Promise<FetchResponse<null>> {
        return await httpPost<IBike>("/bike", this);
    }
    async deleteBikeRequest(): Promise<FetchResponse<null>> {
        return await httpDelete("/bike", this.id);
    }

    clear(): void {
        this.manufacturer = "";
        this.model = "";
        this.mileage = 0.0;
    }

    constructor(...args : IBike[]) {
        if (args.length === 1) {
            let bike = args[0];
            this.id = bike.id;
            this.mileage = bike.mileage;
            this.model = bike.model;
            this.manufacturer = bike.manufacturer;
        }
        else {
            this.id = "";
            this.manufacturer = "";
            this.model = "";
            this.mileage = 0.0;
        }
    }



}

export type BikeResponse = {
    bikes: IBike[];
    errors: string[];
}