import { User } from "./user.model";

export interface UpdateHospitalRequest {
    name: string,
    staff: User[],
}