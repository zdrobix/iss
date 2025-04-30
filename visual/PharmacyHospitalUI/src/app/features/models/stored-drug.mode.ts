import { DrugStorage } from "./drug-storage.model";
import { Drug } from "./drug.model";

export interface StoredDrug {
    id: number,
    quantity: number,
    drug: Drug,
    storage: DrugStorage
}