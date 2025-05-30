import { DrugStorage } from "./drug-storage.model";
import { User } from "./user.model";

export interface Pharmacy {
    id: number,
    name: string,
    storage: DrugStorage
}