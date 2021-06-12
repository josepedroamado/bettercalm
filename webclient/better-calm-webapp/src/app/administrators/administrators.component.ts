import { Administrator } from './../model/administrator';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AdministratorsService } from '../services/administrators/administrators.service';

@Component({
  selector: 'app-administrators',
  templateUrl: './administrators.component.html',
  styleUrls: ['./administrators.component.scss']
})
export class AdministratorsComponent implements OnInit {
  administrators:Administrator[] = [];
  
  constructor(private router: Router, private administratorsService: AdministratorsService) { }

  ngOnInit(): void {
    this.updateAdministrators();
  }

  private updateAdministrators(){
    this.administratorsService.getAll().subscribe((contents) => this.setPsychologists(contents), console.error);
  }
  private setPsychologists(administrators:Administrator[]){
    this.administrators = administrators;
  }

  goToAddView(id: any){
    if(id != null){
      this.router.navigate(['/administrator-add/'+id])
    }
    else{
      this.router.navigate(['/administrator-add']);
    }
  }

  removeAdministrator(administrator: number){
    this.administratorsService.remove(administrator).subscribe();
    this.updateAdministrators();
  }
}
