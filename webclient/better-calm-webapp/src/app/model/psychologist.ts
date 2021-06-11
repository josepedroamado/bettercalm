import { Illness } from "./illness";

export interface Psychologist {
    id: number;
    firstName: string;
    lastName: string;
    address: string;
    format: string;
    illnesses:Illness[];
    rate: number;
}