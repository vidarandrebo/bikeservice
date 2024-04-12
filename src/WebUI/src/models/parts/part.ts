import { DataArrayResponse, FetchResponse, httpDelete, httpGetWithBody, httpPost, httpPut } from "../httpMethods.ts";

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

    async addPartRequest(): Promise<FetchResponse<null>> {
        return await httpPost<Part>("/api/part", this);
    }

    async deletePartRequest(): Promise<FetchResponse<null>> {
        return await httpDelete("/api/part", this.id);
    }

    async putPartRequest(): Promise<FetchResponse<null>> {
        return await httpPut<Part>("/api/part", this);
    }
}

export async function getPartsRequest(): Promise<Part[]> {
    const result = await httpGetWithBody<DataArrayResponse<Part>>("/api/part");
    if (result.status === 200) {
        return result.body.data.map(createPart);
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
