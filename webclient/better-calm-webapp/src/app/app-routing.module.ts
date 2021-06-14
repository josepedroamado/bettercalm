import { AdministratorGuard } from './guards/administrator.guard';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LogoutComponent } from './logout/logout.component';
import { LoginComponent } from './login/login.component';

const routes: Routes = [
  {
    path:'',
    loadChildren: () => import('./public/public.module').then(mod => mod.PublicModule)
  },
  {
    path:'admin',
    canActivate: [AdministratorGuard],
    loadChildren: () => import('./admin/admin.module').then(mod => mod.AdminModule)
  },
  {
      path: 'login',
      component: LoginComponent
  },
  {
    path: 'logout',
    component: LogoutComponent,
    pathMatch: 'full'
  },
  {
    path:'**',
    redirectTo: ''
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
