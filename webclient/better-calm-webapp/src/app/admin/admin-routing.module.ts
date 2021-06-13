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

const routes: Routes = [
    {
        path:'',
        component: ContentManagementComponent,
        children:[
          {
            path:'',
            component: ContentManagementComponent
          },
          {
            path:'contents',
            component: ContentManagementComponent,
            children: [
                {
                    path:'edit',
                    component: ContentEditComponent
                },
                {
                    path:'edit/:id',
                    component: ContentEditComponent
                }
            ]
          },
          {
            path:'psychologists',
            component: PsychologistsComponent,
            children: [
                {
                    path:'edit',
                    component: PsychologistEditComponent
                },
                {
                    path:'edit/:id',
                    component: PsychologistEditComponent
                }
            ]
          },
          {
            path:'administrators',
            component: AdministratorsComponent,
            children: [
                {
                    path:'edit',
                    component: AdministratorEditComponent
                },
                {
                    path:'edit/:email',
                    component: AdministratorEditComponent
                }
            ]
          },
          {
            path:'discounts',
            component: ApprovediscountsComponent,
            children: [
                {
                    path:'add/:email',
                    component: PatientDiscountAddComponent
                }
            ]
          },
          {
            path:'importers',
            component: ImportersComponent
          }
        ]
      }
]

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
  })
  
  export class AdminRoutingModule { }