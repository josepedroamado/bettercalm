import { IllnessIn } from "./illnessIn";

export interface Psychologist {
    id: number;
    firstName: string;
    lastName: string;
    address: string;
    format: string;
    illnessModels:IllnessIn[];
    rate: number;
}