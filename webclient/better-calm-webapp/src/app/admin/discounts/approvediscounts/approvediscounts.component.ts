import { Patient } from '../../../model/patient';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { PatientsService } from '../../../services/patients/patients.service';

@Component({
  selector: 'app-approvediscounts',
  templateUrl: './approvediscounts.component.html',
  styleUrls: ['./approvediscounts.component.scss']
})
export class ApprovediscountsComponent implements OnInit {
  patients:Patient[] = [];
  error = false;
  errorMessage ="No hay pacientes que cumplan los requerimientos para obtener un descuento.";

  constructor(private router: Router, private patientsService: PatientsService) { }

  ngOnInit(): void {
    this.updatePatients();
  }

  private updatePatients(){
    this.patientsService.getAllPatientsWithDiscountsToAprove().subscribe((contents) => this.setPatients(contents), (error : any) => this.showError(error));
  }
  
  private showError(error: any){
    this.error = true;
  }

  private setPatients(patients:Patient[]){
    this.patients = patients;
  }

  approveDiscount(email: string){
    this.router.navigate(['/admin/discounts/add/'+ email]);
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
