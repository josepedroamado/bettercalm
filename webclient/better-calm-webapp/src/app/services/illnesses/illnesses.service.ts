import { IllnessIn } from '../../model/illnessIn';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { catchError } from 'rxjs/operators';
import { BaseService } from '../common/base-service';

@Injectable({
  providedIn: 'root'
})
export class IllnessesService extends BaseService{
  private readonly target_url:string = `${environment.api_url}/illnesses`
  
  constructor(http: HttpClient) {
    super(http);
   }

  public getIllnesses(): Observable<IllnessIn[]> {
    return this.http
      .get<IllnessIn[]>(this.target_url)
      .pipe(catchError(this.handleError));
  }
}
