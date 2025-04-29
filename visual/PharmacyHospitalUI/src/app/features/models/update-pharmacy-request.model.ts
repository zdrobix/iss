import { DrugStorage } from "./drug-storage.model";
import { User } from "./user.model";

export interface UpdatePharmacyRequest {
    name: string,
    storage: DrugStorage
}