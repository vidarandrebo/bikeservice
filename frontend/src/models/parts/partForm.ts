import {FetchResponse, post} from "@/models/genericFetch";

export interface IPartForm {
    manufacturer: string;
    model: string;
    mileage: number;

    clear(): void;

    addPartRequest(): Promise<FetchResponse<null>>;
}

export class PartForm implements IPartForm {
    manufacturer: string;
    model: string;
    mileage: number;

    clear(): void {
        this.mileage = 0.0;
        this.model = "";
        this.manufacturer = "";
    }

    async addPartRequest(): Promise<FetchResponse<null>> {
        return await post<PartForm>("/part", this);
    }


    constructor() {
        this.manufacturer = "";
        this.model = "";
        this.mileage = 0.0;
    }
}