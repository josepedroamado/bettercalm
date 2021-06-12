import { IllnessIn } from "./illnessIn";

export interface PsychologistIn {
    id: number;
    firstName: string;
    lastName: string;
    address: string;
    format: string;
    illnessModels:IllnessIn[];
    rate: number;
}