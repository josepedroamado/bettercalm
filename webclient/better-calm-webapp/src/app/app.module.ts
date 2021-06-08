import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http'; 
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { ContentsComponent } from './contents/contents.component';
import { ButtonComponent } from './button/button.component';
import { ContentCardComponent } from './content-card/content-card.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    ContentsComponent,
    ButtonComponent,
    ContentCardComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
