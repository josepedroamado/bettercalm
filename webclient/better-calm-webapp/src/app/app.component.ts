import { Observable } from 'rxjs';
import { SessionsService } from './services/sessions/sessions.service';
import { Component } from '@angular/core';
import '@popperjs/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'better-calm-webapp';
  userLoggedIn$: Observable<boolean>;

  constructor(private sessionsService: SessionsService) {
    this.userLoggedIn$ = this.sessionsService.getUserLoggedIn();
  }

  logout(){
    let token = localStorage.getItem("token") ?? "";
    this.sessionsService.logOut(token);
  }
}
