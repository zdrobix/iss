import { OrderedDrug } from "./ordered-drug.model";
import { User } from "./user.model";

export interface AddOrderRequest {
    placedBy: User,
    orderedDrugs: OrderedDrug[]
    dateTime: Date
}