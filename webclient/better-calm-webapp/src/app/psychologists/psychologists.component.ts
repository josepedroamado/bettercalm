import { PsychologistsService } from './../services/psychologists/psychologists.service';
import { Component, OnInit } from '@angular/core';
import { PsychologistIn } from '../model/psychologist';
import { Router } from '@angular/router';

@Component({
  selector: 'app-psychologists',
  templateUrl: './psychologists.component.html',
  styleUrls: ['./psychologists.component.scss']
})
export class PsychologistsComponent implements OnInit {
  psychologists:PsychologistIn[] = [];

  constructor(private psychologistsService: PsychologistsService, private router: Router) { }

  ngOnInit(): void {
    this.psychologistsService.getAll().subscribe((contents) => this.setPsychologists(contents), console.error);
  }

  private setPsychologists(psychologists:PsychologistIn[]){
    this.psychologists = psychologists;
  }

  goToAddView(){
    this.router.navigate(['/psychologist-add'])
  }
}
