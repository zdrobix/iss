import { Hospital } from "./hospital.model";
import { Pharmacy } from "./pharmacy.model";

export interface User {
    id: number,
    username: string,
    name: string,
    password: string,
    role: string
    pharmacyId: number | null,
    hospitalId: number | null,
    pharmacy: Pharmacy | null,
    hospital: Hospital | null,
}