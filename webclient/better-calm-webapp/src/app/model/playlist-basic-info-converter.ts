import { Playlist } from "../domain/playlist";
import { PlaylistBasicInfo } from "./playlist-basic-info";

export class PlaylistBasicInfoConverter{
    public static GetDomainPlaylist(basicInfo:PlaylistBasicInfo):Playlist{
        return {
            categories: basicInfo.categories,
            description: basicInfo.description,
            id: basicInfo.id,
            imageUrl: basicInfo.imageUrl,
            name: basicInfo.name,
            contents: []
        };
    }
}