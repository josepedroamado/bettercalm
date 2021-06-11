import { PsychologistsService } from './../services/psychologists/psychologists.service';
import { Component, OnInit } from '@angular/core';
import { Psychologist } from '../model/psychologist';

@Component({
  selector: 'app-psychologists',
  templateUrl: './psychologists.component.html',
  styleUrls: ['./psychologists.component.scss']
})
export class PsychologistsComponent implements OnInit {
  psychologists:Psychologist[] = [];

  constructor(private psychologistsService: PsychologistsService) { }

  ngOnInit(): void {
    this.psychologistsService.getAll().subscribe((contents) => this.setPsychologists(contents), console.error);
  }

  private setPsychologists(psychologists:Psychologist[]){
    this.psychologists = psychologists;
  }
}
