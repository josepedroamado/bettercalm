import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ImportInfo } from '../domain/import-info';
import { ImportTypes } from '../domain/import-types';
import { ImportService } from '../services/import/import.service';

@Component({
  selector: 'app-importers',
  templateUrl: './importers.component.html',
  styleUrls: ['./importers.component.scss']
})
export class ImportersComponent implements OnInit {
  importTypes:string[] = [];
  isLoading:boolean = false;
  hasError:boolean = false;
  errorMessage:string = "";

  public importForm = this.formBuilder.group(
    {
      type: ['', Validators.required],
      filePath: ['', Validators.required]
    });

  constructor(private importService:ImportService, 
    private formBuilder: FormBuilder) { }

  ngOnInit(): void {
    this.hasError = false;
    this.getImportTypes();
  }

  private getImportTypes():void{
    this.isLoading = true;
    this.importService.getTypes().subscribe((importTypes:ImportTypes) => this.setImportTypes(importTypes))
  }

  private setImportTypes(importTypes:ImportTypes):void{
    this.importTypes = importTypes.types;
    if (this.importTypes.length > 0){
      let currentFormValue = this.importForm.value;
      currentFormValue.type = this.importTypes[0];
      this.importForm.patchValue(currentFormValue);  
    }
    else{
      this.setError("No hay importadores disponibles.")
    }
    this.isLoading = false;
  }

  public onSubmit(input:ImportInfo):void{
    this.importService.post(input).subscribe(
      () => this.setOk(), 
      (error) => this.setError(error));
  };

  private setOk():void{
    alert("Enviado correctamente");
  }

  private setError(error:string):void{
    this.hasError = true;
    this.errorMessage = error;
  }
}
