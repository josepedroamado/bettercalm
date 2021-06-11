import { BaseService } from './../common/base-service';
import { LoginIn } from './../../model/loginIn';
import { LoginOut } from './../../model/loginOut';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { EventEmitter, Injectable, Output, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class SessionsService extends BaseService {
  private readonly target_url:string = `${environment.api_url}/sessions`
  @Output() sendLoggedInEvent = new EventEmitter<boolean>(true);
  
  constructor(http: HttpClient, private router: Router) { 
    super(http);
  }

  login(loginOut: LoginOut): Observable<LoginIn> {
    return this.http.post<LoginIn>(this.target_url, loginOut)
      .pipe(catchError(this.handleError), map(user => this.processLogin(user)));
  }

  isLogged(){
    return localStorage.getItem('token') != null;
  }

  private processLogin(user:LoginIn){
    localStorage.setItem('token', user.token);
    this.emitLoggedStatus(true);
    return user;
  }

  emitLoggedStatus(loggedIn: boolean) {
    this.sendLoggedInEvent.emit(loggedIn);
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
    this.emitLoggedStatus(false);
    this.router.navigate(['/home'])
  }
}
