import { PsychologistsService } from './../services/psychologists/psychologists.service';
import { Component, OnInit } from '@angular/core';
import { Psychologist } from '../model/psychologist';
import { Router } from '@angular/router';

@Component({
  selector: 'app-psychologists',
  templateUrl: './psychologists.component.html',
  styleUrls: ['./psychologists.component.scss']
})
export class PsychologistsComponent implements OnInit {
  psychologists:Psychologist[] = [];

  constructor(private psychologistsService: PsychologistsService, private router: Router) { }

  ngOnInit(): void {
    this.updatePsychologists();
  }

  private updatePsychologists(){
    this.psychologistsService.getAll().subscribe((contents) => this.setPsychologists(contents), console.error);
  }
  private setPsychologists(psychologists:Psychologist[]){
    this.psychologists = psychologists;
  }

  goToAddView(id: any){
    if(id != null){
      this.router.navigate(['/psychologist-add/'+id])
    }
    else{
      this.router.navigate(['/psychologist-add']);
    }
  }

  removePsychologist(psychologist: number){
    this.psychologistsService.remove(psychologist).subscribe(() => this.updatePsychologists());
  }
}
