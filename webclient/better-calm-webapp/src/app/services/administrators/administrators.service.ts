import { Administrator } from './../../model/administrator';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { BaseService } from '../common/base-service';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AdministratorsService extends BaseService{
  private readonly target_url:string = `${environment.api_url}/administrators`;

  constructor(http: HttpClient) {
    super(http);
  }

  public getAll(): Observable<Administrator[]> {
    let options = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': localStorage.getItem("token") ?? ""
      })
    };
    return this.http
      .get<Administrator[]>(this.target_url, options)
      .pipe(catchError(this.handleError));
  }

  public post(input: Administrator): Observable<Administrator>{
    let options = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': localStorage.getItem("token") ?? ""
      })
    };
    return this.http.post<Administrator>(this.target_url, input, options)
    .pipe(catchError(this.handleError) );
  }

  public patch(input: Administrator): Observable<Administrator>{
    let options = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': localStorage.getItem("token") ?? ""
      })
    };
    return this.http.patch<Administrator>(this.target_url, input, options)
    .pipe(catchError(this.handleError) );
  }

  public remove(input: number): Observable<unknown>{
    let options = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': localStorage.getItem("token") ?? ""
      }),
      body: {
        id: input
      },
    };
    return this.http.delete<any>(this.target_url, options)
    .pipe(catchError(this.handleError));
  }
}
