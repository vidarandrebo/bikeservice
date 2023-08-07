import {DataArrayResponse, FetchResponse, httpDelete, httpGetWithBody, httpPost, httpPut} from "../httpMethods.ts";

export class Bike {
    id: string;
    manufacturer: string;
    model: string;
    mileage: number;
    typeId: string;
    date: Date;


    async addBikeRequest(): Promise<FetchResponse<null>> {
        return await httpPost<Bike>("/api/bike", this);
    }

    async deleteBikeRequest(): Promise<FetchResponse<null>> {
        return await httpDelete("/api/bike", this.id);
    }

    async putBikeRequest(): Promise<FetchResponse<null>> {
        return await httpPut<Bike>("/api/bike", this);
    }

    clear(): void {
        this.manufacturer = "";
        this.model = "";
        this.mileage = 0.0;
        this.typeId = "";
    }

    // Copies over the fields in the argument object if given
    constructor(...args: Bike[]) {
        this.id = "";
        this.manufacturer = "";
        this.model = "";
        this.mileage = 0.0;
        this.typeId = "";
        this.date = new Date();
        if (args.length === 1) {
            Object.assign(this, args[0]);
            this.date = new Date(args[0].date)
        }
    }
}

export async function getBikesRequest(): Promise<Bike[]> {
    const result = await httpGetWithBody<DataArrayResponse<Bike>>("/api/bike");
    if (result.status === 200) {
        return result.body.data.map(createBike);
    }
    return [];
}


function createBike(bike: Bike) {
    return new Bike(bike);
}
