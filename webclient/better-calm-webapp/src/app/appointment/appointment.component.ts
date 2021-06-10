import { AppointmentOut } from 'src/app/model/appointmentOut';
import { AppointmentsService } from './../services/appointments/appointments.service';
import { Component, OnInit } from '@angular/core';
import { Illness } from '../model/illness';
import { IllnessesService } from '../services/illnesses/illnesses.service';
import { FormBuilder, Validators } from '@angular/forms';
import { AppointmentIn } from '../model/appointmentIn';

@Component({
  selector: 'app-appointment',
  templateUrl: './appointment.component.html',
  styleUrls: ['./appointment.component.scss'],
  providers: [ IllnessesService ]
})
export class AppointmentComponent implements OnInit {
  illnesses:Illness[] = [];
  today = new Date();
  submitted = false;
  psychologistName:string = "";
  appointmentData: AppointmentIn = new AppointmentIn();

  appointmentForm = this.formBuilder.group(
    {
      name: ['', Validators.required],
      lastName: ['', Validators.required],
      birthDate: ['', Validators.required],
      eMail: ['', [Validators.required, Validators.email]],
      phone: ['', [Validators.required, Validators.pattern("[0-9]{3,9}")]],
      illnessId: [null, Validators.required],
      duration: ['', Validators.required],
    });
  
  constructor(private illnessesService: IllnessesService, private formBuilder: FormBuilder, private appointmentsService: AppointmentsService) { }

  ngOnInit(): void {
    this.illnessesService.getIllnesses().subscribe(
        ((data : Array<Illness>) => this.setIllnesses(data)),
        ((error : any) => console.log(error))
      );
  }

  private setIllnesses(data: Array<Illness>):void {
    this.illnesses = data;
  }

  onSubmit(input: any){
    let newAppointment : AppointmentOut = new AppointmentOut(input.illnessId, input.name, input.lastName, input.birthDate, input.eMail, input.phone, input.duration)
    console.warn(newAppointment);
    this.appointmentsService.postAppoinment(newAppointment).subscribe(
      ((data : AppointmentIn) => this.showPostReturn(data)),
      ((error : any) => console.log(error))
    );
    this.submitted = true;
  }

  private showPostReturn(data:AppointmentIn){
    this.appointmentData = data;
  } 
}
