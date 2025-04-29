import { Hospital } from "./hospital.model";
import { Pharmacy } from "./pharmacy.model";

export interface User {
    id: number,
    username: string,
    name: string,
    password: string,
    role: string,
    pharmacy: Pharmacy | null,
    hospital: Hospital | null,
}