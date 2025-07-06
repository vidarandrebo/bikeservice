import { getBikeClient } from "../Api.ts";
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
        const client = getBikeClient();
        try {
            await client.apiBikePost({ postBikeRequest: this });
            return 201;
        } catch {
            console.log("failed to add bike");
        }
        return -1;
    }

    async deleteBikeRequest(): Promise<void> {
        const client = getBikeClient();

        await client.apiBikeDelete({ id: this.id });
    }

    async putBikeRequest(): Promise<number> {
        const client = getBikeClient();

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
    const client = getBikeClient();
    try {
        const response = await client.apiBikeGet();
        return response.map(Bike.fromResponse);
    } catch {
        console.log("failed to load bikes");
    }
    return [];
}
