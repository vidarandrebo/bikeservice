import { ref, Ref } from "vue";
import { Bike } from "./Bike.ts";

export type BikeCollection = {
    bikes: Ref<Bike[]>;
    addBike: (value: Bike) => void;
    fetchBikes: () => void;
};

export function DefaultBikeCollection(): BikeCollection {
    return {
        bikes: ref<Bike[]>([]),
        fetchBikes: () => {},
        addBike: () => {}
    };
}
