import { User } from "./user.model";

export interface Hospital {
    id: number,
    name: string,
    staff: User[]
}