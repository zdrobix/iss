import { OrderedDrug } from "./ordered-drug.model"
import { User } from "./user.model"

export interface Order {
    placedBy: User,
        resolvedBy: User | null,
        orderedDrugs: OrderedDrug[]
        dateTime: Date
}