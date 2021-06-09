import { TimeStamp } from "./time-stamp";

export interface ContentBasicInfo{
    id: number;
    name: string;
    artistName: string;
    imageUrl: string;
    contentUrl: string;
    contentType: string;
    contentLength: TimeStamp;
}