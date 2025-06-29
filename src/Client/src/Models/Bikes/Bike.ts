import { HttpRequest } from "http-methods-ts";
import { loadBearerTokenFromLocalStorage } from "../Auth/User.ts";
import { getClient } from "../ApiClient.ts";
import { RequestConfiguration } from "@microsoft/kiota-abstractions";
import { BikeRequestBuilderDeleteQueryParameters } from "../../../gen/api/bike";

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
        const client = getClient();

        const cfg: RequestConfiguration<BikeRequestBuilderDeleteQueryParameters> = {
            queryParameters: {
                id: this.id
            }
        };

        await client.api.bike.delete(cfg);

    }

    async putBikeRequest(): Promise<number> {
        const client = getClient();

        const response = await client.api.bike.put(this);

        if (response) {
            return 200;
        }
        return -1;
    }

    clear(): void {
        this.manufacturer = "";
        this.model = "";
        this.mileage = 0.0;
        this.typeId = "";
    }
}

export async function getBikesRequest(): Promise<Bike[]> {
    const client = getClient();

    const httpResponse = await client.api.bike.get();
    if (httpResponse) {
        const payload = httpResponse as Bike[];
        return payload.map(createBike);
    }
    return [];
}

function createBike(bike: Bike) {
    return new Bike(bike);
}
