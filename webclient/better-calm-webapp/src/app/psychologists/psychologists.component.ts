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
    this.psychologistsService.getAll().subscribe((contents) => this.setPsychologists(contents), console.error);
  }

  private setPsychologists(psychologists:Psychologist[]){
    this.psychologists = psychologists;
  }

  goToAddView(){
    this.router.navigate(['/psychologist-add'])
  }

  removePsychologist(psychologist: Psychologist){
    this.psychologistsService.remove(psychologist);
    this.router.navigate(['/psychologists']);
  }
}
