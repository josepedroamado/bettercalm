import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient} from '@angular/common/http';
import { Observable } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { Content } from 'src/app/domain/content';
import { ContentBasicInfo } from 'src/app/model/content-basic-info';
import { ContentBasicInfoConverter } from 'src/app/model/content-basic-info-converter';
import { BaseService } from '../common/base-service';

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

  private convertBasicInfoContents(contentsBasicInfo:ContentBasicInfo[]):Content[]{
    return contentsBasicInfo.map(content => ContentBasicInfoConverter.GetDomainContent(content));
  }
}
