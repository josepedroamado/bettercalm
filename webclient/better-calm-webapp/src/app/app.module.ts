import { AdministratorGuard } from './guards/administrator.guard';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http'; 
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ContentsComponent } from './contents/contents.component';
import { ContentCardComponent } from './content-card/content-card.component';
import { ContentDetailComponent } from './content-detail/content-detail.component';
import { ContentDetailAudioComponent } from './content-detail-audio/content-detail-audio.component';
import { ContentDetailVideoComponent } from './content-detail-video/content-detail-video.component';
import { AppointmentComponent } from './appointment/appointment.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './login/login.component';
import { LogoutComponent } from './logout/logout.component';
import { NgSelectModule } from '@ng-select/ng-select'; 
import { NgOptionHighlightModule } from '@ng-select/ng-option-highlight';
import { PlaylistsComponent } from './playlists/playlists.component';
import { CategoriesComponent } from './categories/categories.component';
import { UtilsModule } from './utils/utils.module';

@NgModule({
  declarations: [
    AppComponent,
    ContentsComponent,
    ContentCardComponent,
    ContentDetailComponent,
    ContentDetailAudioComponent,
    ContentDetailVideoComponent,
    AppointmentComponent,
    LoginComponent,
    LogoutComponent,
    PlaylistsComponent,
    CategoriesComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    NgSelectModule,
    NgOptionHighlightModule,
    FormsModule,
    UtilsModule
  ],
  providers: [AdministratorGuard],
  bootstrap: [AppComponent]
})
export class AppModule { }
