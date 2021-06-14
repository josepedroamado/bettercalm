import { PsychologistsService } from '../../../services/psychologists/psychologists.service';
import { Component, OnInit } from '@angular/core';
import { Psychologist } from '../../../model/psychologist';
import { Router } from '@angular/router';

@Component({
  selector: 'app-psychologists',
  templateUrl: './psychologists.component.html',
  styleUrls: ['./psychologists.component.scss']
})
export class PsychologistsComponent implements OnInit {
  psychologists:Psychologist[] = [];
  error = false;

  constructor(private psychologistsService: PsychologistsService, private router: Router) { }

  ngOnInit(): void {
    this.updatePsychologists();
  }

  private updatePsychologists(){
    this.psychologistsService.getAll().subscribe((contents) => this.setPsychologists(contents), (error : any) => this.showError(error));
  }
  private setPsychologists(psychologists:Psychologist[]){
    this.psychologists = psychologists;
  }

  private showError(error: any){
    this.error = true;
  }

  goToAddView(id: any){
    if(id != null){
      this.router.navigate(['/admin/psychologists/edit/'+id])
    }
    else{
      this.router.navigate(['/admin/psychologists/edit']);
    }
  }

  removePsychologist(psychologist: number){
    this.psychologistsService.remove(psychologist).subscribe(() => this.updatePsychologists());
  }
}
