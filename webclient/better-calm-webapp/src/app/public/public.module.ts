import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ContentsModule } from './contents/contents.module';
import { AppointmentComponent } from './appointment/appointment.component';
import { CategoriesService } from '../services/categories/categories.service';
import { ContentsService } from '../services/contents/contents.service';
import { BaseService } from '../services/common/base-service';
import { IndexPublicComponent } from './index-public/index-public.component';
import { PublicRoutingModule } from './public-routing.module';
import { UtilsModule } from '../utils/utils.module';
import { PlaylistsComponent } from './playlists/playlists.component';
import { NavbarComponent } from '../public/navbar/navbar.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgSelectModule } from '@ng-select/ng-select';
import { NgOptionHighlightModule } from '@ng-select/ng-option-highlight';



@NgModule({
  declarations: [
    AppointmentComponent,
    IndexPublicComponent,
    PlaylistsComponent,
    NavbarComponent
  ],
  imports: [
    CommonModule,
    ContentsModule,
    PublicRoutingModule,
    ReactiveFormsModule,
    NgSelectModule,
    NgOptionHighlightModule,
    FormsModule,
    UtilsModule
  ],
  providers: [
    ContentsService,
    CategoriesService,
    BaseService
  ]
})
export class PublicModule { }
