export interface IBikeForm {
    manufacturer: string;
    model: string;
    mileage: number;
}
export class BikeForm {
    manufacturer: string;
    model: string;
    mileage: number;
    constructor() {
        this.manufacturer = "";
        this.model = "";
        this.mileage = 0;
    }
}