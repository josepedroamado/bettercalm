import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http'; 
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { ContentsComponent } from './contents/contents.component';
import { ButtonComponent } from './button/button.component';
import { ContentCardComponent } from './content-card/content-card.component';
import { ContentDetailComponent } from './content-detail/content-detail.component';
import { ContentDetailAudioComponent } from './content-detail-audio/content-detail-audio.component';
import { ContentDetailVideoComponent } from './content-detail-video/content-detail-video.component';
import { AppointmentComponent } from './appointment/appointment.component';
import { ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './login/login.component';
import { LogoutComponent } from './logout/logout.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    ContentsComponent,
    ButtonComponent,
    ContentCardComponent,
    ContentDetailComponent,
    ContentDetailAudioComponent,
    ContentDetailVideoComponent,
    AppointmentComponent,
    LoginComponent,
    LogoutComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
