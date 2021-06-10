import { Content } from '../domain/content';
import { ContentBasicInfo } from './content-basic-info';
import { TimeStampConverter } from './time-stamp-converter';

export class ContentBasicInfoConverter{
    public static GetDomainContent(contentBasicInfo:ContentBasicInfo):Content{
        let content:Content = {
            artistName: contentBasicInfo.artistName,
            contentLength: TimeStampConverter.GetDomainTimeStamp(contentBasicInfo.contentLength),
            contentType: contentBasicInfo.contentType,
            contentUrl: contentBasicInfo.contentUrl,
            id: contentBasicInfo.id,
            imageUrl: contentBasicInfo.imageUrl,
            name: contentBasicInfo.name,
            categories: contentBasicInfo.categories
        }
        return content;
    }
}