import { PlaylistBasicInfo } from "./playlist-basic-info";

export interface ModelContent{
    id: number;
    name: string;
    artistName: string;
    imageUrl: string;
    contentUrl: string;
    contentType: string;
    contentLength: string;
    categories: number[];
    playlists: PlaylistBasicInfo[]
}