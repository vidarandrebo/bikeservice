import { ref, Ref } from "vue";
import { Part } from "./Part.ts";

export type PartCollection = {
    parts: Ref<Part[]>;
    addPart: (value: Part) => void;
    fetchParts: () => void;
};

export function DefaultPartCollection(): PartCollection {
    return {
        parts: ref<Part[]>([]),
        fetchParts: () => {},
        addPart: () => {}
    };
}
