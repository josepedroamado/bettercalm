import { LoginIn } from './../../model/loginIn';
import { LoginOut } from './../../model/loginOut';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class SessionsService {
  private readonly target_url:string = `${environment.api_url}/sessions`
  private loggedIn = false;

  constructor(private http: HttpClient, private router: Router) { 
    let storedToken = localStorage.getItem('token');
    if (storedToken != null){
      this.loggedIn = true;
    }
  }

  login(loginOut: LoginOut): Observable<LoginIn> {
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

  logOut(user: string): void {
    const options = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
      body: {
        Token: user,
      },
    };
    
    this.http
      .delete(this.target_url, options)
      .subscribe((s) => {
        console.log(s);
      });
    localStorage.removeItem("token");
    this.router.navigate(['/home'])
  }
}
