import { HttpRequest } from "http-methods-ts";
import { loadBearerTokenFromLocalStorage } from "../Auth/User.ts";

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
        const bearerToken = loadBearerTokenFromLocalStorage();
        if (bearerToken === null) {
            return;
        }
        const httpRequest = new HttpRequest()
            .setRoute("api/bike")
            .setMethod("DELETE")
            .addHeader("Content-Type", "application/json")
            .addUrlParam("id", this.id)
            .setBearerToken(bearerToken);
        await httpRequest.send();
    }

    async putBikeRequest(): Promise<number> {
        const bearerToken = loadBearerTokenFromLocalStorage();
        if (bearerToken === null) {
            return -1;
        }
        const httpRequest = new HttpRequest()
            .setRoute("api/bike")
            .setMethod("PUT")
            .addHeader("Content-Type", "application/json")
            .setBearerToken(bearerToken)
            .setRequestData(this);
        await httpRequest.send();
        const response = httpRequest.getResponseData();
        if (response) {
            return response.status;
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
    const bearerToken = loadBearerTokenFromLocalStorage();
    if (bearerToken === null) {
        return [];
    }
    const httpRequest = new HttpRequest()
        .setRoute("/api/bike")
        .setMethod("GET")
        .setBearerToken(bearerToken)
        .addHeader("Content-Type", "application/json");

    await httpRequest.send();

    const httpResponse = httpRequest.getResponseData();
    if (httpResponse != null) {
        if (httpResponse.status == 200) {
            const payload = httpResponse.body as Bike[];
            return payload.map(createBike);
        }
    }
    return [];
}

function createBike(bike: Bike) {
    return new Bike(bike);
}
