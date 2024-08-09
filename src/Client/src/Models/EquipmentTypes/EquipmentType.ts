import { Category } from "./Category.ts";
import { loadBearerTokenFromLocalStorage } from "../Auth/User.ts";
import { HttpRequest } from "http-methods-ts";

export class EquipmentType {
    id: string;
    name: string;
    category: Category;

    constructor(...args: EquipmentType[]) {
        this.id = "";
        this.name = "";
        this.category = Category.Bike;
        if (args.length === 1) {
            Object.assign(this, args[0]);
        }
    }

    async addTypeRequest(): Promise<number> {
        const bearerToken = loadBearerTokenFromLocalStorage();
        if (bearerToken === null) {
            return -1;
        }
        const httpRequest = new HttpRequest()
            .setRoute(window.location.origin + "/" + "api/type")
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

    clear() {
        this.name = "";
        this.category = Category.Bike;
        this.id = "";
    }
}

/**
 * Acquires all equipment-types and returns an array of object containing them.
 */
export async function getTypeRequest(): Promise<EquipmentType[]> {
    const bearerToken = loadBearerTokenFromLocalStorage();
    if (bearerToken === null) {
        return [];
    }
    const httpRequest = new HttpRequest()
        .setRoute("/api/type")
        .setMethod("GET")
        .setBearerToken(bearerToken)
        .addHeader("Content-Type", "application/json");

    await httpRequest.send();

    const httpResponse = httpRequest.getResponseData();
    if (httpResponse != null) {
        if (httpResponse.status == 200) {
            const payload = httpResponse.body as EquipmentType[];
            return payload.map(createType);
        }
    }
    return [];
}

/**
 * Mapper for the types received from api-call
 * @param type The received object
 */
function createType(type: EquipmentType) {
    return new EquipmentType(type);
}
