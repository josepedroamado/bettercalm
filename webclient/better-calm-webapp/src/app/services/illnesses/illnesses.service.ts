import { Illness } from '../../model/illness';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class IllnessesService {
  private readonly target_url:string = `${environment.api_url}/illnesses`
  
  constructor(private http: HttpClient) { }

  public getIllnesses(): Observable<Illness[]> {
    return this.http
      .get<Illness[]>(this.target_url)
      .pipe(catchError(this.handleError));
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
}
