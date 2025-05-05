import { DrugStorage } from "./drug-storage.model";
import { Drug } from "./drug.model";

export interface AddStoredDrugRequest {
    drug: Drug,
    quantity: number,
    storageId: number
}