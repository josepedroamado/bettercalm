import { SessionsService } from './services/sessions/sessions.service';
import { Component, OnInit } from '@angular/core';
import '@popperjs/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'better-calm-webapp';
  loggedIn = false;

  constructor(private sessionsService: SessionsService) {
  }
  
  ngOnInit(): void {
    this.loggedIn = this.sessionsService.isLogged();
    this.sessionsService.sendLoggedInEvent.subscribe((data:boolean) => {this.loggedIn = data;});
  }

  logout(){
    let token = localStorage.getItem("token") ?? "";
    this.sessionsService.logOut(token);
  }
}
