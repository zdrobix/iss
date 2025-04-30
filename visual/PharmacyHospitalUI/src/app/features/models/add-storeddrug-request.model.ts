import { DrugStorage } from "./drug-storage.model";
import { Drug } from "./drug.model";

export interface AddStoredDrugRequest {
    quantity: number,
    drug: Drug,
    storage: DrugStorage
}