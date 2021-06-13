import { AdministratorGuard } from './guards/administrator.guard';
import { LogoutComponent } from './logout/logout.component';
import { LoginComponent } from './login/login.component';
import { AppointmentComponent } from './appointment/appointment.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ContentsComponent } from './contents/contents.component';
import { PlaylistsComponent } from './playlists/playlists.component';

const routes: Routes = [
  {
    path:'admin',
    canActivate: [AdministratorGuard],
    loadChildren: () => import('./admin/admin.module').then(mod => mod.AdminModule)
  },
  {
    path: '',
    redirectTo: 'home',
    pathMatch: 'full'
  },
  {
    path: 'home',
    component: ContentsComponent,
    pathMatch: 'full'
  },
  {
    path: 'contents',
    component: ContentsComponent,
    pathMatch: 'full'
  },
  {
    path: 'playlists',
    component: PlaylistsComponent,
    pathMatch: 'full'
  },
  {
    path: 'playlists/contents/:playlistId',
    component: ContentsComponent,
    pathMatch: 'full',
  },
  {
    path: 'appointment',
    component: AppointmentComponent,
    pathMatch: 'full'
  },
  {
    path: 'login',
    component: LoginComponent,
    pathMatch: 'full'
  },
  {
    path: 'logout',
    component: LogoutComponent,
    pathMatch: 'full'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
