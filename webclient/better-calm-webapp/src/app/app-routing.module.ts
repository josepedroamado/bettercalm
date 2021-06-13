import { AdministratorGuard } from './guards/administrator.guard';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path:'admin',
    canActivate: [AdministratorGuard],
    loadChildren: () => import('./admin/admin.module').then(mod => mod.AdminModule)
  },
  {
    path:'',
    loadChildren: () => import('./public/public.module').then(mod => mod.PublicModule)
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
