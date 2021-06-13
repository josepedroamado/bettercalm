import { Content } from "./content";

export interface Playlist{
    id:number;
    name:string;
    description:string;
    imageUrl:string;
    categories:number[],
    contents:Content[]
}