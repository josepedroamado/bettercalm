import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminRoutingModule } from './admin-routing.module';
import { AdministratorsService } from '../services/administrators/administrators.service';
import { ContentsService } from '../services/contents/contents.service';
import { PsychologistsService } from '../services/psychologists/psychologists.service';
import { PatientsService } from '../services/patients/patients.service';
import { CategoriesService } from '../services/categories/categories.service';
import { ImportService } from '../services/import/import.service';
import { BaseService } from '../services/common/base-service';
import { ShowErrorComponent } from '../utils/show-error/show-error.component';
import { LoadingComponent } from '../utils/loading/loading.component';
import { AdministratorEditComponent } from './administrator/administrator-edit/administrator-edit.component';
import { AdministratorsComponent } from './administrator/administrators/administrators.component';
import { ContentEditComponent } from './contents/content-edit/content-edit.component';
import { ContentManagementComponent } from './contents/content-management/content-management.component';
import { ApprovediscountsComponent } from './discounts/approvediscounts/approvediscounts.component';
import { PatientDiscountAddComponent } from './discounts/patient-discount-add/patient-discount-add.component';
import { ImportersComponent } from './importers/importers/importers.component';
import { PsychologistEditComponent } from './psychologists/psychologist-edit/psychologist-edit.component';
import { PsychologistsComponent } from './psychologists/psychologists/psychologists.component';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgSelectModule } from '@ng-select/ng-select';
import { NgOptionHighlightModule } from '@ng-select/ng-option-highlight';
import { UtilsModule } from '../utils/utils.module';
import { IndexAdminComponent } from './index-admin/index-admin.component';



@NgModule({
  declarations: [
    AdministratorEditComponent,
    AdministratorsComponent,
    ContentEditComponent,
    ContentManagementComponent,
    ApprovediscountsComponent,
    PatientDiscountAddComponent,
    ImportersComponent,
    PsychologistEditComponent,
    PsychologistsComponent,
    IndexAdminComponent
  ],
  imports: [
    CommonModule,
    AdminRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    NgSelectModule,
    NgOptionHighlightModule,
    FormsModule,
    UtilsModule
  ],
  providers: [
    AdministratorsService,
    ContentsService,
    PsychologistsService,
    PatientsService,
    CategoriesService,
    ImportService,
    BaseService
  ]
})
export class AdminModule { }
