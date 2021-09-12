import { NgModule } from '@angular/core';
import { FormsModule , ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { ToastrModule } from 'ngx-toastr';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { HttpClientModule } from '@angular/common/http';
import { RegisterComponent } from './register/register.component';
import { UserManagmentComponent } from './user-managment/user-managment.component';
import { AllUserManagmentComponent } from './all-user-managment/all-user-managment.component';
import { ChangePasswordComponent } from './change-password/change-password.component';
import { AddRoleComponent } from './add-role/add-role.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { OwlDateTimeModule, OwlNativeDateTimeModule } from 'ng-pick-datetime';
import { TotalHoursComponent } from './reports/total-hours/total-hours.component';
import { DailyHoursComponent } from './reports/daily-hours/daily-hours.component';
import { AbsentComponent } from './reports/absent/absent.component';
import { BasicComponent } from './basic/basic.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    UserManagmentComponent,
    AllUserManagmentComponent,
    ChangePasswordComponent,
    AddRoleComponent,
    TotalHoursComponent,
    DailyHoursComponent,
    AbsentComponent,
    BasicComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    OwlDateTimeModule, 
    OwlNativeDateTimeModule,
    ToastrModule.forRoot({
      timeOut:2000,
      progressBar:true,
      progressAnimation:'increasing',
      preventDuplicates:true
    })
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
