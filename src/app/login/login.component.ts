import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { User } from '../Models/User';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  public loginForm = this.formBuilder.group({
    email:['',[Validators.email, Validators.required]],
    password:['',Validators.required]
  })
  public isAlert= false;
  public alertMessage= "";
  constructor(private formBuilder:FormBuilder , private userService : UserService , private router : Router , private toaster: ToastrService) { }

  ngOnInit(): void {
      if(this.user != null){
        this.router.navigate(["/alluser-managment"]);
      }
  }
  onSubmit(){
    let email=this.loginForm.controls["email"].value
    let password=this.loginForm.controls["password"].value
    this.userService.login( email , password).subscribe((data:any)=>{
    if(data.responseCode == 1 ){
      localStorage.setItem("userInfo",JSON.stringify(data.dataSet));
      let user = data.dataSet as User;
      this.toaster.success("Welcome Back "+user.userName , "Successfully login");
      if(user.checked == 0){
        this.router.navigate(["/change-password"]);
      }
      else if(user.role == "Admin"){
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
  }
  get user():User{
    return JSON.parse(localStorage.getItem("userInfo")) as User 
 }
}
