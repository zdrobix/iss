import { StoredDrug } from "./stored-drug.mode";

export interface DrugStorage {
    id: number,
    storedDrugs: StoredDrug[]
}