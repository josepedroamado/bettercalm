import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { AdministratorEditComponent } from "./administrator/administrator-edit/administrator-edit.component";
import { AdministratorsComponent } from "./administrator/administrators/administrators.component";
import { ApprovediscountsComponent } from "./discounts/approvediscounts/approvediscounts.component";
import { ContentEditComponent } from "./contents/content-edit/content-edit.component";
import { ContentManagementComponent } from "./contents/content-management/content-management.component";
import { ImportersComponent } from "./importers/importers/importers.component";
import { PatientDiscountAddComponent } from "./discounts/patient-discount-add/patient-discount-add.component";
import { PsychologistEditComponent } from "./psychologists/psychologist-edit/psychologist-edit.component";
import { PsychologistsComponent } from "./psychologists/psychologists/psychologists.component";
import { IndexAdminComponent } from "./index-admin/index-admin.component";

const routes: Routes = [
    {
        path:'',
        component: IndexAdminComponent,
        children:[
          {
            path:'',
            component: ContentManagementComponent
          },
          {
            path:'contents',
            component: ContentManagementComponent
          },
          {
            path:'contents/edit',
            component: ContentEditComponent
          },
          {
            path:'contents/edit/:id',
            component: ContentEditComponent
          },
          {
            path:'psychologists',
            component: PsychologistsComponent
          },
          {
            path:'psychologists/edit',
            component: PsychologistEditComponent
          },
          {
            path:'psychologists/edit/:id',
            component: PsychologistEditComponent
          },
          {
            path:'administrators',
            component: AdministratorsComponent
          },
          {
            path:'administrators/edit',
            component: AdministratorEditComponent
          },
          {
            path:'administrators/edit/:email',
            component: AdministratorEditComponent
          },
          {
            path:'discounts',
            component: ApprovediscountsComponent
          },
          {
            path:'discounts/add/:email',
            component: PatientDiscountAddComponent
          },
          {
            path:'importers',
            component: ImportersComponent
          },
          {
            path:'**',
            redirectTo: ''
          }
        ]
      }
]

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
  })
  
  export class AdminRoutingModule { }