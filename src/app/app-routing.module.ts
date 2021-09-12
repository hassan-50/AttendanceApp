import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddRoleComponent } from './add-role/add-role.component';
import { AllUserManagmentComponent } from './all-user-managment/all-user-managment.component';
import { BasicComponent } from './basic/basic.component';
import { ChangePasswordComponent } from './change-password/change-password.component';
import { AuthGuardService } from './guards/auth.service';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { AbsentComponent } from './reports/absent/absent.component';
import { DailyHoursComponent } from './reports/daily-hours/daily-hours.component';
import { TotalHoursComponent } from './reports/total-hours/total-hours.component';
import { UserManagmentComponent } from './user-managment/user-managment.component';

const routes: Routes = [
  {path:"",component:BasicComponent},
  {path:"login",component:LoginComponent},
  {path:"register",component:RegisterComponent},
  {path:"user-managment",component:UserManagmentComponent, canActivate:[AuthGuardService]},
  {path:"alluser-managment",component:AllUserManagmentComponent, canActivate:[AuthGuardService]},
  {path:"change-password",component:ChangePasswordComponent,canActivate:[AuthGuardService]},
  {path:"add-role",component:AddRoleComponent,canActivate:[AuthGuardService]},
  {path:"total-hours",component:TotalHoursComponent,canActivate:[AuthGuardService]},
  {path:"daily-hours",component:DailyHoursComponent,canActivate:[AuthGuardService]},
  {path:"absent",component:AbsentComponent,canActivate:[AuthGuardService]},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
