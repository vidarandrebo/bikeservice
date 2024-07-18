import { ref, Ref } from "vue";
import { Bike } from "./Bike.ts";

export type BikesDependency = {
    bikes: Ref<Bike[]>;
    addBike: (value: Bike) => void;
    setBikes: (value: Bike[]) => void;
};

export function DefaultBikeDependency(): BikesDependency {
    return {
        bikes: ref<Bike[]>([]),
        setBikes: () => {},
        addBike: () => {}
    };
}
