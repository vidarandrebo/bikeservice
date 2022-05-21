import {FetchResponse, httpDelete, httpPost} from "@/models/httpMethods";

export interface IPart {
    id: string;
    manufacturer: string;
    model: string;
    mileage: number;

    addPartRequest(): Promise<FetchResponse<null>>;

    deletePartRequest(): Promise<FetchResponse<null>>;

    clear(): void;
}

export class Part implements IPart {
    id: string;
    manufacturer: string;
    model: string;
    mileage: number;

    clear(): void {
        this.id = "";
        this.mileage = 0.0;
        this.model = "";
        this.manufacturer = "";
    }

    async addPartRequest(): Promise<FetchResponse<null>> {
        return await httpPost<Part>("/part", this);
    }

    async deletePartRequest(): Promise<FetchResponse<null>> {
        return await httpDelete("/part", this.id);
    }


    constructor(...args: IPart[]) {
        this.id = "";
        this.manufacturer = "";
        this.model = "";
        this.mileage = 0.0;
        if (args.length === 1) {
            Object.assign(this, args[0]);
        }
    }
}

export function createPart(part: IPart) {
    return new Part(part);
}