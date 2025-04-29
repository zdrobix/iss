import { Drug } from "./drug.model";

export interface OrderedDrug {
    quantity: number,
    drug: Drug
}