import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { User } from '../Models/User';
import { UserService } from '../services/user.service';
// import { MustMatch } from './_helpers/must-match.validator';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.scss']
})
export class ChangePasswordComponent implements OnInit {
  public loginForm = this.formBuilder.group({
    oldpassword:['',Validators.required],
    password:['',Validators.required],
    confirmpassword:['',Validators.required]
  })
  public isAlert= false;
  public alertMessage= "";

  constructor(private formBuilder:FormBuilder , private userService:UserService ,private router:Router , private toaster:ToastrService) { }

  ngOnInit(): void {
  }

  onSubmit(){
    let oldpassword=this.loginForm.controls["oldpassword"].value
    let newpassword=this.loginForm.controls["password"].value
    let confirmpassword=this.loginForm.controls["confirmpassword"].value
    if(newpassword != confirmpassword){
      this.isAlert = true;
        this.alertMessage = "password and confirmPassword must match";
    }
    this.userService.changePassword(oldpassword , newpassword).subscribe((data)=>{ 
           let usere = JSON.parse(localStorage.getItem("userInfo")) as User
      this.userService.login( usere.email, newpassword).subscribe((data:any)=>{
        if(data.responseCode == 1 ){
          this.isAlert = false;
          localStorage.setItem("userInfo",JSON.stringify(data.dataSet));
          let user = data.dataSet as User;
          this.toaster.success("password had changed" , "Password changing");
           if(user.role == "Admin"){
            this.router.navigate(["/user-managment"]);
          }
          else{
            this.router.navigate(["/alluser-managment"]);
          }
        }
        
      },error =>{
        this.isAlert = true;
        this.alertMessage = error.error.responseMessage;
      })
    },error =>{
      this.isAlert = true;
      this.alertMessage = error.error.responseMessage;

    })
  }

}
