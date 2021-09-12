import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { User } from '../Models/User';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-add-role',
  templateUrl: './add-role.component.html',
  styleUrls: ['./add-role.component.scss']
})
export class AddRoleComponent implements OnInit {

  public loginForm = this.formBuilder.group({
    role:['',Validators.required]
  })
  public isAlert= false;
  public alertMessage= "";
  constructor(private formBuilder:FormBuilder , private userService:UserService ,private router:Router , private toaster :ToastrService) { }

  ngOnInit(): void {
  }

  onSubmit(){
    let role=this.loginForm.controls["role"].value
    this.userService.addRole(role).subscribe((data)=>{ 
           let user = JSON.parse(localStorage.getItem("userInfo")) as User      
      this.toaster.success("Role Added Successfuly" , "Adding Role");
      if(user.role == "Admin"){
        this.router.navigate(["/user-managment"]);
      }
      else{
        this.router.navigate(["/alluser-managment"]);
      }
    } ,error=>{
      this.isAlert = true;
    this.alertMessage = error.error.responseMessage;
    })
  }
}
