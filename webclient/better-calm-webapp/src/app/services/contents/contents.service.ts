import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpErrorResponse} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { Content } from 'src/app/domain/content';
import { ContentBasicInfo } from 'src/app/model/content-basic-info';
import { ContentBasicInfoConverter } from 'src/app/model/content-basic-info-converter';

@Injectable({
  providedIn: 'root'
})
export class ContentsService {
  private readonly target_url:string = `${environment.api_url}/contents`
  
  constructor(private http: HttpClient) { }

  public getAll(): Observable<Content[]> {
    return this.http
      .get<ContentBasicInfo[]>(this.target_url)
      .pipe(catchError(this.handleError), map(this.convertBasicInfoContents));
  }

  private handleError(error: HttpErrorResponse) {
    if (error.status === 0) {
      return throwError("Server is shut down");
    }
    else {
      return throwError(
        error.error ? error.error.message : "Server problems, try later");
    }
  }

  private convertBasicInfoContents(contentsBasicInfo:ContentBasicInfo[]):Content[]{
    return contentsBasicInfo.map(content => ContentBasicInfoConverter.GetDomainContent(content));
  }
}
