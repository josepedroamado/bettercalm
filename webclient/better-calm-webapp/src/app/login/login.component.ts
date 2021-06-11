import { LoginIn } from './../model/loginIn';
import { SessionsService } from './../services/sessions/sessions.service';
import { LoginOut } from './../model/loginOut';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit{
  loginForm = this.formBuilder.group(
    {
      eMail: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required]
    });
  loginError = false;
  errorMessage ="";
  loggedIn = false;

  constructor(private formBuilder: FormBuilder, 
              private sessionsService: SessionsService,
              private router: Router) { }

  ngOnInit(): void {
    this.loggedIn = this.sessionsService.isLogged();
    if(this.loggedIn){
      this.router.navigate(['/home']);
    }
  }

  onSubmit(input: LoginOut){
    this.sessionsService.login(input).subscribe(
      ((data : LoginIn) => this.router.navigate(['/home'])),
      ((error : any) => this.showError(error))
    );
  }

  private showError(error: any){
      this.loginError = true;
      this.errorMessage = error;
  }
}
