import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient} from '@angular/common/http';
import { Observable } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { Content } from 'src/app/domain/content';
import { ContentBasicInfo } from 'src/app/model/content-basic-info';
import { ContentBasicInfoConverter } from 'src/app/model/content-basic-info-converter';
import { ModelContent } from 'src/app/model/model-content';
import { BaseService } from '../common/base-service';
import { ModelContentConverter } from 'src/app/model/model-content-converter';

@Injectable({
  providedIn: 'root'
})
export class ContentsService extends BaseService{
  private readonly target_url:string = `${environment.api_url}/contents`

  constructor(http: HttpClient) {
    super(http);
  }

  public getAll(): Observable<Content[]> {
    return this.http
      .get<ContentBasicInfo[]>(this.target_url)
      .pipe(catchError(this.handleError), map(this.convertBasicInfoContents));
  }

  public get(id:string): Observable<Content> {
    let getContentUrl:string = this.target_url+"/"+id;
    return this.http
      .get<ModelContent>(getContentUrl)
      .pipe(catchError(this.handleError), map(this.convertModelContent));
  }

  private convertBasicInfoContents(contentsBasicInfo:ContentBasicInfo[]):Content[]{
    return contentsBasicInfo.map(content => ContentBasicInfoConverter.GetDomainContent(content));
  }

  private convertModelContent(modelContent:ModelContent):Content{
    return ModelContentConverter.GetDomainContent(modelContent);
  }
}
