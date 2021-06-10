import { Component, OnInit } from '@angular/core';
import { SessionsService } from '../services/sessions/sessions.service';

@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html',
  styleUrls: ['./logout.component.scss']
})
export class LogoutComponent implements OnInit {

  ngOnInit(): void {
    this.logout();
  }
  
  constructor(private sessionsService: SessionsService) {
  }

  logout(){
    let token = localStorage.getItem("token") ?? "";
    this.sessionsService.logOut(token);
  }
}
