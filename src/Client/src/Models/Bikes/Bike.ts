import { HttpRequest } from "http-methods-ts";
import { loadBearerTokenFromLocalStorage } from "../Auth/User.ts";
import { getBikeApi } from "../Api.ts";
import { BikeResponse } from "../../Gen";

export class Bike {
    id: string;
    manufacturer: string;
    model: string;
    mileage: number;
    typeId: string;
    date: Date;

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
            this.date = new Date(args[0].date);
        }
    }

    get fullName(): string {
        return this.manufacturer + " " + this.model;
    }

    async addBikeRequest(): Promise<number> {
        const bearerToken = loadBearerTokenFromLocalStorage();
        if (bearerToken === null) {
            return -1;
        }
        const httpRequest = new HttpRequest()
            .setRoute("api/bike")
            .setMethod("POST")
            .addHeader("Content-Type", "application/json")
            .setBearerToken(bearerToken)
            .setRequestData(this);
        await httpRequest.send();
        const result = httpRequest.getResponseData();
        if (result) {
            return result.status;
        }
        return -1;
    }

    async deleteBikeRequest(): Promise<void> {
        const client = getBikeApi();

        await client.apiBikeDelete({ id: this.id });
    }

    async putBikeRequest(): Promise<number> {
        const client = getBikeApi();

        try {
            await client.apiBikePut({ putBikeRequest: this });
            return 200;
        } catch {
            console.log("failed to delete bike");
        }
        return -1;
    }

    clear(): void {
        this.manufacturer = "";
        this.model = "";
        this.mileage = 0.0;
        this.typeId = "";
    }

    static fromResponse(response: BikeResponse) {
        const bike = new Bike();
        bike.id = response.id;
        bike.manufacturer = response.manufacturer;
        bike.model = response.model;
        bike.mileage = response.mileage;
        bike.typeId = response.typeId;
        bike.date = response.date;
        return bike;
    }
}

export async function getBikesRequest(): Promise<Bike[]> {
    const client = getBikeApi();
    try {
        const response = await client.apiBikeGet();
        return response.map(Bike.fromResponse);
    } catch {
        console.log("failed to load bikes");
    }
    return [];
}
