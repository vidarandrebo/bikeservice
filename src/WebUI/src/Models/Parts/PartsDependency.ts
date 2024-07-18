import { ref, Ref } from "vue";
import { Part } from "./Part.ts";

export type PartsDependency = {
    parts: Ref<Part[]>;
    addPart: (value: Part) => void;
    setParts: (value: Part[]) => void;
};

export function DefaultPartsDependency(): PartsDependency {
    return {
        parts: ref<Part[]>([]),
        setParts: () => {},
        addPart: () => {}
    };
}
