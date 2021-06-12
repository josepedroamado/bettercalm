import { Playlist } from "./playlist";

export interface Content {
    id: number;
    name: string;
    contentLength: string;
    artistName: string;
    imageUrl: string;
    contentUrl: string;
    contentType: string;
    categories: number[];
    playlists: Playlist[]
}