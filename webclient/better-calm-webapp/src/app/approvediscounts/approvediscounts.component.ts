import { Patient } from './../model/patient';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { PatientsService } from '../services/patients/patients.service';

@Component({
  selector: 'app-approvediscounts',
  templateUrl: './approvediscounts.component.html',
  styleUrls: ['./approvediscounts.component.scss']
})
export class ApprovediscountsComponent implements OnInit {
  patients:Patient[] = [];

  constructor(private router: Router, private patientsService: PatientsService) { }

  ngOnInit(): void {
    this.updatePatients();
  }

  private updatePatients(){
    this.patientsService.getAllPatientsWithDiscountsToAprove().subscribe((contents) => this.setPatients(contents), console.error);
  }
  
  private setPatients(patients:Patient[]){
    this.patients = patients;
  }

  approveDiscount(email: string){
    this.router.navigate(['/patient-discount-add/'+ email]);
  }

  denyDiscount(email: string){
    let patientToUpdate: Patient = {} as any;
    for(let patient of this.patients){
      if(patient.email == email){
        patientToUpdate = patient;
      }
    }
    patientToUpdate.appointmentQuantity = 0;
    this.patientsService.patch(patientToUpdate).subscribe(() => this.updatePatients());
  }
}
