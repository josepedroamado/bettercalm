import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpErrorResponse} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ContentsService {
  private readonly target_url:string = `${environment.api_url}/contents`
  
  constructor(private http: HttpClient) { }

  public getAll(): Observable<any[]> {
    return this.http
      .get<any[]>(this.target_url)
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
