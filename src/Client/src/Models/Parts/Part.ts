import { loadBearerTokenFromLocalStorage } from "../Auth/User.ts";
import { HttpRequest } from "http-methods-ts";

export class Part {
    id: string;
    manufacturer: string;
    model: string;
    mileage: number;
    typeId: string;
    bikeId: string;

    constructor(...args: Part[]) {
        this.id = "";
        this.manufacturer = "";
        this.model = "";
        this.mileage = 0.0;
        this.typeId = "";
        this.bikeId = "";
        if (args.length === 1) {
            Object.assign(this, args[0]);
        }
    }

    clear(): void {
        this.id = "";
        this.mileage = 0.0;
        this.model = "";
        this.manufacturer = "";
        this.typeId = "";
        this.bikeId = "";
    }

    get fullName(): string {
        return this.manufacturer + " " + this.model;
    }

    async addPartRequest(): Promise<number> {
        const bearerToken = loadBearerTokenFromLocalStorage();
        if (bearerToken === null) {
            return -1;
        }
        const httpRequest = new HttpRequest()
            .setRoute("api/part")
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

    async deletePartRequest(): Promise<void> {
        const bearerToken = loadBearerTokenFromLocalStorage();
        if (bearerToken === null) {
            return;
        }
        const httpRequest = new HttpRequest()
            .setRoute(window.location.origin + "/" + "api/part")
            .setMethod("DELETE")
            .addHeader("Content-Type", "application/json")
            .addUrlParam("id", this.id)
            .setBearerToken(bearerToken);
        await httpRequest.send();
    }

    async putPartRequest(): Promise<number> {
        const bearerToken = loadBearerTokenFromLocalStorage();
        if (bearerToken === null) {
            return -1;
        }
        const httpRequest = new HttpRequest()
            .setRoute(window.location.origin + "/" + "api/part")
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
}

export async function getPartsRequest(): Promise<Part[]> {
    const bearerToken = loadBearerTokenFromLocalStorage();
    if (bearerToken === null) {
        return [];
    }
    const httpRequest = new HttpRequest()
        .setRoute("/api/part")
        .setMethod("GET")
        .setBearerToken(bearerToken)
        .addHeader("Content-Type", "application/json");

    await httpRequest.send();

    const httpResponse = httpRequest.getResponseData();
    if (httpResponse != null) {
        if (httpResponse.status == 200) {
            const payload = httpResponse.body as Part[];
            return payload.map(createPart);
        }
    }
    return [];
}

/**
 * Creates a new object of the Part class from an object conforming to the IPart interface
 * @param part The object whose fields will be transferred to new object
 * @returns A new object of the Part class
 */
function createPart(part: Part) {
    return new Part(part);
}
