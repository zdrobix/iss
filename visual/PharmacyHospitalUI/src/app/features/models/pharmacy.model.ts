import { StoredDrug } from "./stored-drug.mode";
import { User } from "./user.model";

export interface Pharmacy {
    id: number,
    name: string,
    staff: User[],
    storage: StoredDrug[],
}