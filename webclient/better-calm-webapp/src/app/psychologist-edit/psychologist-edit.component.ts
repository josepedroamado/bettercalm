import { Psychologist } from 'src/app/model/psychologist';
import { IllnessOut } from '../model/illnessOut';
import { PsychologistsService } from '../services/psychologists/psychologists.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { IllnessIn } from '../model/illnessIn';
import { IllnessesService } from '../services/illnesses/illnesses.service';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-psychologist-edit',
  templateUrl: './psychologist-edit.component.html',
  styleUrls: ['./psychologist-edit.component.scss']
})
export class PsychologistEditComponent implements OnInit {
  isAModification = false;
  illnesses:IllnessIn[] = [];
  selectedIllnesses: number[] = [];
  psychologistId = 0;
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
  title = "Agregar Psicólogo";
  buttonText = "Agregar";

  constructor(private illnessesService: IllnessesService, 
    private psychologistsService: PsychologistsService, 
    private formBuilder: FormBuilder, 
    private router: Router,
    private currentRoute: ActivatedRoute) { }

  ngOnInit(): void {
    let id = this.currentRoute.snapshot.params['id'];
    if(id != null){
      this.psychologistId = id;
      this.isAModification = true;
      this.changeToModifyUI();
      this.loadStoredPsychologist(id);
    }
    this.loadStoredIllnesses();
  }

  private changeToModifyUI(){
    this.title = "Modificar Psicólogo";
    this.buttonText = "Guardar"
  }

  private loadStoredPsychologist(psychologistId: number){
    this.psychologistsService.get(psychologistId).subscribe(
      ((data : Psychologist) => this.loadPsychologistInfoToForm(data)),
      ((error : any) => console.log(error)));
  }

  private loadPsychologistInfoToForm(input : Psychologist){
    this.selectedIllnesses = this.illnessModelToNumberArray(input.illnessModels);
    this.psychologistForm.patchValue({
      firstName: input.firstName,
      lastName: input.lastName,
      address: input.address,
      format: input.format,
      rate: input.rate,
    });
  }

  private illnessModelToNumberArray(illnesses: IllnessIn[]){
    let illnessNames = [];
    for(let illness of illnesses){
      illnessNames.push(illness.id);
    }
    return illnessNames;
  }

  private loadStoredIllnesses(){
    this.illnessesService.getIllnesses().subscribe(
      ((data : Array<IllnessIn>) => this.illnesses = data),
      ((error : any) => console.log(error))
    );
  }

  onSubmit(input: any){
    let illnesses:IllnessOut[] = []
    input.illnessModels.forEach((illness: number) => illnesses.push(this.convertToIllnessOut(illness)));
    input.illnessModels = illnesses;
    input.rate = +input.rate;
    if(this.isAModification){
      input.id = +this.psychologistId;
      this.psychologistsService.patch(input).subscribe(
        (() => this.goBackToListView()),
        ((error : any) => console.log(error))
      );
    }
    else{
      this.psychologistsService.post(input).subscribe(
        (() => this.goBackToListView()),
        ((error : any) => console.log(error))
      );
    }
  }

  convertToIllnessOut(identifier:number){
    let illness:IllnessOut = {
      id : identifier
    }
    return illness;
  }

  goBackToListView(){
    this.router.navigate(['/psychologists'])
  }
}
