import { AdministratorEditComponent } from './administrator-edit/administrator-edit.component';
import { PsychologistEditComponent } from './psychologist-edit/psychologist-edit.component';
import { AdministratorGuard } from './guards/administrator.guard';
import { ImportersComponent } from './importers/importers.component';
import { ApprovediscountsComponent } from './approvediscounts/approvediscounts.component';
import { AdministratorsComponent } from './administrators/administrators.component';
import { PsychologistsComponent } from './psychologists/psychologists.component';
import { LogoutComponent } from './logout/logout.component';
import { LoginComponent } from './login/login.component';
import { AppointmentComponent } from './appointment/appointment.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ContentsComponent } from './contents/contents.component';
import { ContentEditComponent } from './content-edit/content-edit.component';
import { PlaylistsComponent } from './playlists/playlists.component';

const routes: Routes = [
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
    path: 'contents/edit',
    component: ContentEditComponent,
    pathMatch: 'full',
    canActivate: [AdministratorGuard]
  },
  {
    path: 'contents/edit/:id',
    component: ContentEditComponent,
    pathMatch: 'full',
    canActivate: [AdministratorGuard]
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
    path: 'psychologists',
    component: PsychologistsComponent,
    canActivate: [AdministratorGuard],
    pathMatch: 'full'
  },
  {
    path: 'psychologist-edit',
    component: PsychologistEditComponent,
    canActivate: [AdministratorGuard],
    pathMatch: 'full'
  },
  {
    path: 'psychologist-edit/:id',
    component: PsychologistEditComponent,
    canActivate: [AdministratorGuard],
    pathMatch: 'full'
  },
  {
    path: 'administrators',
    component: AdministratorsComponent,
    canActivate: [AdministratorGuard],
    pathMatch: 'full'
  },
  {
    path: 'administrator-edit',
    component: AdministratorEditComponent,
    canActivate: [AdministratorGuard],
    pathMatch: 'full'
  },
  {
    path: 'administrator-edit/:email',
    component: AdministratorEditComponent,
    canActivate: [AdministratorGuard],
    pathMatch: 'full'
  },
  {
    path: 'approvediscounts',
    component: ApprovediscountsComponent,
    canActivate: [AdministratorGuard],
    pathMatch: 'full'
  },
  {
    path: 'importers',
    component: ImportersComponent,
    canActivate: [AdministratorGuard],
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
