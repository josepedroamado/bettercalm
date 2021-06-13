import { AdministratorGuard } from './guards/administrator.guard';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http'; 
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './login/login.component';
import { LogoutComponent } from './logout/logout.component';
import { NgSelectModule } from '@ng-select/ng-select'; 
import { NgOptionHighlightModule } from '@ng-select/ng-option-highlight';
import { UtilsModule } from './utils/utils.module';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    LogoutComponent
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
