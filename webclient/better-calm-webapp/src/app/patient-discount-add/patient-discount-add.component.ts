import { PatientsService } from './../services/patients/patients.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Patient } from '../model/patient';

@Component({
  selector: 'app-patient-discount-add',
  templateUrl: './patient-discount-add.component.html',
  styleUrls: ['./patient-discount-add.component.scss']
})
export class PatientDiscountAddComponent implements OnInit {
  patientDiscountForm = this.formBuilder.group(
    {
      discount: ["15", Validators.required]
    });
  patient:Patient = {} as any;
  errorOccurred = false;
  errorMessage ="";

  constructor(private patientsService: PatientsService, 
    private formBuilder: FormBuilder, 
    private router: Router,
    private currentRoute: ActivatedRoute) { }

  ngOnInit(): void {
    let email = this.currentRoute.snapshot.params['email'];
    if(email != undefined){
      this.loadStoredPatient(email);
    }
  }

  private loadStoredPatient(patientEmail: string){
    this.patientsService.get(patientEmail).subscribe(
      ((data : Patient) => this.patient = data),
      ((error : any) => console.log(error)));
  }

  onSubmit(input: any){
      this.patient.appointmentDiscount = +input.discount;
      this.patientsService.patch(this.patient).subscribe(
        (() => this.goBackToListView()),
        ((error : any) => this.showError(error))
      );
  }

  goBackToListView(){
    this.router.navigate(['/approvediscounts']);
  }

  private showError(error: any){
    this.errorOccurred = true;
    this.errorMessage = error;
    this.loadStoredPatient(this.patient.email);
  }
}
