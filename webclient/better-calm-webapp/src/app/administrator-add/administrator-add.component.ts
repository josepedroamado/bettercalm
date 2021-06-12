import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Administrator } from '../model/administrator';
import { AdministratorsService } from '../services/administrators/administrators.service';

@Component({
  selector: 'app-administrator-add',
  templateUrl: './administrator-add.component.html',
  styleUrls: ['./administrator-add.component.scss']
})
export class AdministratorAddComponent implements OnInit {
  isAModification = false;
  administratorId = 0;
  administratorForm = this.formBuilder.group(
    {
      email: ["", Validators.required],
      name: ["", Validators.required],
      password: ["", Validators.required]
    });
  title = "Agregar Administrador";
  buttonText = "Agregar";

  constructor(private administratorsService: AdministratorsService, 
    private formBuilder: FormBuilder, 
    private router: Router,
    private currentRoute: ActivatedRoute) { }

  ngOnInit(): void {
    let id = this.currentRoute.snapshot.params['id'];
    if(id != null){
      this.administratorId = id;
      this.isAModification = true;
      this.changeToModifyUI();
      this.loadStoredAdministrator(id);
    }
  }

  private changeToModifyUI(){
    this.title = "Modificar administrador";
    this.buttonText = "Guardar"
  }

  private loadStoredAdministrator(administratorEmail: string){
    this.administratorsService.get(administratorEmail).subscribe(
      ((data : Administrator) => this.loadPsychologistInfoToForm(data)),
      ((error : any) => console.log(error)));
  }

  private loadPsychologistInfoToForm(input : Administrator){
    this.administratorForm.patchValue({
      email: input.email,
      name: input.name,
    });
  }

  onSubmit(input: any){
    if(this.isAModification){
      input.id = +this.administratorId;
      this.administratorsService.patch(input).subscribe(
        (() => this.goBackToListView()),
        ((error : any) => console.log(error))
      );
    }
    else{
      this.administratorsService.post(input).subscribe(
        (() => this.goBackToListView()),
        ((error : any) => console.log(error))
      );
    }
  }

  goBackToListView(){
    this.router.navigate(['/administrators'])
  }
}
