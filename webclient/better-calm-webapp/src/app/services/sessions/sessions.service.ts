import { LoginIn } from './../../model/loginIn';
import { LoginOut } from './../../model/loginOut';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class SessionsService {
  private readonly target_url:string = `${environment.api_url}/sessions`
  private loggedIn = false;

  constructor(private http: HttpClient) { 
    let storedToken = localStorage.getItem('token');
    if (storedToken != null){
      this.loggedIn = true;
    }
  }

  public login(loginOut: LoginOut): Observable<LoginIn> {
    return this.http.post<LoginIn>(this.target_url, loginOut)
      .pipe(catchError(this.handleError), map(user => this.processLogin(user)));
  }

  private processLogin(user:LoginIn){
    localStorage.setItem('token', user.token);
    return user;
  }

  private handleError(errorRequest: any) {
    console.error(errorRequest);
    return throwError(errorRequest.error || errorRequest.message);
  }

  getUserLoggedIn():Observable<boolean> {
    return new Observable((observer) => {
      observer.next(this.loggedIn);
    });
  }
}
