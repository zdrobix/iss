import { OrderedDrug } from "./ordered-drug.model"
import { User } from "./user.model"

export interface Order {
    id: number,
    placedBy: User,
    resolvedBy: User | null,
    orderedDrugs: OrderedDrug[]
    dateTime: Date
}