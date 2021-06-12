import { Content } from "../domain/content";
import { ModelContent } from "./model-content";
import { PlaylistBasicInfoConverter } from "./playlist-basic-info-converter";

export class ModelContentConverter{
    public static GetDomainContent(modelContent:ModelContent):Content{
        return {
            artistName: modelContent.artistName,
            categories: modelContent.categories,
            contentLength: modelContent.contentLength,
            contentType: modelContent.contentType,
            contentUrl: modelContent.contentUrl,
            id: modelContent.id,
            imageUrl: modelContent.imageUrl,
            name: modelContent.name,
            playlists: modelContent.playlists.map(playlist => 
                PlaylistBasicInfoConverter.GetDomainPlaylist(playlist))
        };
    }
}