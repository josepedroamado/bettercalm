import { BaseService } from './../common/base-service';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AppointmentOut } from 'src/app/model/appointmentOut';
import { environment } from 'src/environments/environment';
import { catchError } from 'rxjs/operators';
import { AppointmentIn } from 'src/app/model/appointmentIn';

@Injectable({
  providedIn: 'root'
})
export class AppointmentsService extends BaseService {
  private readonly target_url:string = `${environment.api_url}/appointments`

  constructor(http: HttpClient) { 
    super(http);
  }

  public postAppoinment(newAppointment: AppointmentOut): Observable<AppointmentIn> {
    return this.http.post<AppointmentIn>(this.target_url, newAppointment)
      .pipe(catchError(this.handleError));
  }
}
