import { IllnessOut } from './../model/illnessOut';
import { PsychologistsService } from '../services/psychologists/psychologists.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { IllnessIn } from '../model/illnessIn';
import { IllnessesService } from '../services/illnesses/illnesses.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-psychologist-edit',
  templateUrl: './psychologist-add.component.html',
  styleUrls: ['./psychologist-add.component.scss']
})
export class PsychologistAddComponent implements OnInit {
  illnesses:IllnessIn[] = [];
  psychologistForm = this.formBuilder.group(
    {
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      address: ['', Validators.required],
      format: ['OnSite', Validators.required],
      illnessModels: [[], Validators.required],
      rate: ['500', Validators.required],
    });
  maxItems = 3;

  constructor(private illnessesService: IllnessesService, 
    private psychologistsService: PsychologistsService, 
    private formBuilder: FormBuilder, 
    private router: Router) { }

  ngOnInit(): void {
    this.illnessesService.getIllnesses().subscribe(
      ((data : Array<IllnessIn>) => this.setIllnesses(data)),
      ((error : any) => console.log(error))
    );
  }

  private setIllnesses(data: Array<IllnessIn>):void {
    this.illnesses = data;
  }

  onSubmit(input: any){
    let illnesses:IllnessOut[] = []
    input.illnessModels.forEach((illness: number) => illnesses.push(this.convertToIllnessOut(illness)));
    input.illnessModels = illnesses;
    input.rate = +input.rate;
    this.psychologistsService.post(input).subscribe(
      (() => this.goBackToListView()),
      ((error : any) => console.log(error))
    );
  }

  goBackToListView(){
    this.router.navigate(['/psychologists'])
  }

  convertToIllnessOut(identifier:number){
    let illness:IllnessOut = {
      id : identifier
    }
    return illness;
  }
}
