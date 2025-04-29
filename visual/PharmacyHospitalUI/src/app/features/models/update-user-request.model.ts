import { Hospital } from "./hospital.model";
import { Pharmacy } from "./pharmacy.model";

export interface UpdateUserRequest {
    name: string,
    username: string,
    password: string,
    role: string,
    pharmacy: Pharmacy | null,
    hospital: Hospital | null,
}