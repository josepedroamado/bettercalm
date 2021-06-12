import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { PsychologistIn as Psychologist } from 'src/app/model/psychologist';
import { environment } from 'src/environments/environment';
import { BaseService } from '../common/base-service';

@Injectable({
  providedIn: 'root'
})
export class PsychologistsService extends BaseService{
    private readonly target_url:string = `${environment.api_url}/psychologists`
    
    constructor(http: HttpClient) {
      super(http);
  }

  public getAll(): Observable<Psychologist[]> {
    let options = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': localStorage.getItem("token") ?? ""
      })
    };
    return this.http
      .get<Psychologist[]>(this.target_url, options)
      .pipe(catchError(this.handleError));
  }

  public post(input: Psychologist): Observable<Psychologist>{
    let options = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': localStorage.getItem("token") ?? ""
      })
    };
    return this.http.post<Psychologist>(this.target_url, input, options)
    .pipe(catchError(this.handleError) );
  }

  public patch(input: Psychologist): Observable<Psychologist>{
    let options = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': localStorage.getItem("token") ?? ""
      })
    };
    return this.http.patch<Psychologist>(this.target_url, input, options)
    .pipe(catchError(this.handleError) );
  }
}
