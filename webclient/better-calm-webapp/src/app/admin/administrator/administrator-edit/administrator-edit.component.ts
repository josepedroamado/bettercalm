import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Administrator } from '../../../model/administrator';
import { AdministratorsService } from '../../../services/administrators/administrators.service';

@Component({
  selector: 'app-administrator-edit',
  templateUrl: './administrator-edit.component.html',
  styleUrls: ['./administrator-edit.component.scss']
})
export class AdministratorEditComponent implements OnInit {
  isAModification = false;
  administratorEmail = "";
  administratorId = 0;
  errorOccurred = false;
  errorMessage ="";
  administratorForm = this.formBuilder.group(
    {
      email: ['', [Validators.required, Validators.email]],
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
    let email = this.currentRoute.snapshot.params['email'];
    if(email != undefined){
      this.administratorEmail = email;
      this.isAModification = true;
      this.changeToModifyUI();
      this.loadStoredAdministrator(email);
    }
  }

  private changeToModifyUI(){
    this.title = "Modificar administrador";
    this.buttonText = "Guardar"
  }

  private loadStoredAdministrator(administratorEmail: string){
    this.administratorsService.get(administratorEmail).subscribe(
      ((data : Administrator) => this.loadAdministratorInfoToForm(data)),
      ((error : any) => console.log(error)));
  }

  private loadAdministratorInfoToForm(input : Administrator){
    this.administratorId = input.id;
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
        ((error : any) => this.showError(error))
      );
    }
    else{
      this.administratorsService.post(input).subscribe(
        (() => this.goBackToListView()),
        ((error : any) => this.showError(error))
      );
    }
  }

  goBackToListView(){
    this.router.navigate(['/admin/administrators']);
  }

  private showError(error: any){
    this.errorOccurred = true;
    this.errorMessage = error;
  }
}
