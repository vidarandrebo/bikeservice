export type Bike = {
    id: string;
    manufacturer: string;
    model: string;
    mileage: number;
}
export type BikeResponse = {
    bikes: Bike[];
    errors: string[];
}