import { getPartClient } from "../Api.ts";
import { PartResponse } from "../../Gen";

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

    async addPartRequest(): Promise<Part | null> {
        const client = getPartClient();
        try {
            const response = await client.apiPartPost({ postPartRequest: this });
            return Part.fromResponse(response);
        } catch {
            console.log("failed to add part");
        }
        return null;
    }

    async deletePartRequest(): Promise<void> {
        const client = getPartClient();
        try {
            await client.apiPartDelete({ id: this.id });
        } catch {
            console.log("failed to delete part");
        }
    }

    async putPartRequest(): Promise<number> {
        const client = getPartClient();

        try {
            await client.apiPartPut({ putPartRequest: this });
            return 200;
        } catch {
            console.log("failed to edit part");
        }
        return -1;
    }
    static fromResponse(response: PartResponse): Part {
        const part = new Part();
        part.id = response.id;
        part.bikeId = response.bikeId;
        part.manufacturer = response.manufacturer;
        part.model = response.model;
        part.mileage = response.mileage;
        return part;
    }
}

export async function getPartsRequest(): Promise<Part[]> {
    const client = getPartClient();
    try {
        const response = await client.apiPartGet();
        return response.map(Part.fromResponse);
    } catch {
        console.log("failed to fetch parts");
    }
    return [];
}
