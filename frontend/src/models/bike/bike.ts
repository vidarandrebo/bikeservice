import {Part} from "@/models/bike/part";

export type Bike = {
    id: string;
    manufacturer: string;
    model: string;
    parts: Part[];

}