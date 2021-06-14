import { Administrator } from '../../../model/administrator';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AdministratorsService } from '../../../services/administrators/administrators.service';

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
    this.administratorsService.getAll().subscribe((contents) => this.setAdministrators(contents), console.error);
  }
  
  private setAdministrators(administrators:Administrator[]){
    this.administrators = administrators;
  }

  goToAddView(email: string){
    this.router.navigate(['/admin/administrators/edit/'+ email]);
  }

  removeAdministrator(administrator: number){
    this.administratorsService.remove(administrator).subscribe(() => this.updateAdministrators());
  }
}
