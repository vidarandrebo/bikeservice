import {FetchResponse, post} from "@/models/genericFetch";

export interface IBikeForm {
    manufacturer: string;
    model: string;
    mileage: number;
    addBikeRequest(): Promise<FetchResponse<null>>;
    clear(): void;
}
export class BikeForm {
    manufacturer: string;
    model: string;
    mileage: number;
    async addBikeRequest() : Promise<FetchResponse<null>> {
        return await post<BikeForm>("/bike", this);
    }
    clear() : void {
        this.manufacturer = "";
        this.model = "";
        this.mileage = 0.0;
    }
    constructor() {
        this.manufacturer = "";
        this.model = "";
        this.mileage = 0;
    }
}