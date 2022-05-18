export type Part = {
    id: string;
    manufacturer: string;
    model: string;
}
export type PartResponse = {
    parts: Part[];
    errors: string[];
}