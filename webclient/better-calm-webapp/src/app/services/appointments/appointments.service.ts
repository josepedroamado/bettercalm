import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { AppointmentOut } from 'src/app/model/appointmentOut';
import { environment } from 'src/environments/environment';
import { catchError } from 'rxjs/operators';
import { AppointmentIn } from 'src/app/model/appointmentIn';

@Injectable({
  providedIn: 'root'
})
export class AppointmentsService {
  private readonly target_url:string = `${environment.api_url}/appointments`

  constructor(private http: HttpClient) { }

  public postAppoinment(newAppointment: AppointmentOut): Observable<AppointmentIn> {
    return this.http.post<AppointmentIn>(this.target_url, newAppointment)
      .pipe(catchError(this.handleError));
  }

  private handleError(errorRequest: any) {
    console.error(errorRequest);
    return throwError(errorRequest.error || errorRequest.message);
  }
}
