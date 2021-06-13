import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Patient } from 'src/app/model/patient';
import { environment } from 'src/environments/environment';
import { BaseService } from '../common/base-service';

@Injectable({
  providedIn: 'root'
})
export class PatientsService extends BaseService{
  private readonly target_url:string = `${environment.api_url}/patients`

  constructor(http: HttpClient) {
    super(http);
  }

  public getAllPatientsWithDiscountsToAprove(): Observable<Patient[]> {
    let options = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': localStorage.getItem("token") ?? ""
      })
    };
    return this.http
      .get<Patient[]>(this.target_url+'/approvediscounts', options)
      .pipe(catchError(this.handleError));
  }

  public get(email: string): Observable<Patient> {
    let options = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': localStorage.getItem("token") ?? ""
      })
    };
    return this.http
      .get<Patient>(this.target_url + '/' + email, options)
      .pipe(catchError(this.handleError));
  }

  public patch(input: Patient): Observable<Patient>{
    let options = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': localStorage.getItem("token") ?? ""
      })
    };
    return this.http.patch<Patient>(this.target_url, input, options)
    .pipe(catchError(this.handleError) );
  }
}
