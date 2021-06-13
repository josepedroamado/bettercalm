import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoadingComponent } from './loading/loading.component';
import { ShowErrorComponent } from './show-error/show-error.component';



@NgModule({
  declarations: [
    LoadingComponent,
    ShowErrorComponent
  ],
  imports: [
    CommonModule
  ],
  exports: [
    LoadingComponent,
    ShowErrorComponent
  ]
})
export class UtilsModule { }
