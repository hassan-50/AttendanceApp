import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Role } from '../Models/role';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  public roles:Role[] =[]; 
  public registerForm = this.formBuilder.group({
    fullname:['', Validators.required],
    username:['', Validators.required],
    email:['',[Validators.email, Validators.required]],
    password:['',Validators.required]
  })
  public isAlert= false;
  public alertMessage= "";

  constructor(private formBuilder:FormBuilder,private userService:UserService , private toaster : ToastrService) { }

  ngOnInit(): void {
    this.getAllRoles();
  }
  onSubmit(){
    console.log("on submit", this.roles)    
    let fullname=this.registerForm.controls["fullname"].value
    let username=this.registerForm.controls["username"].value
    let email=this.registerForm.controls["email"].value
    let password=this.registerForm.controls["password"].value    
  this.userService.register(fullname , username , email , password,this.roles.filter(x=> x.isSelected)[0].role).subscribe((data)=>{
    this.roles.forEach(x=> x.isSelected= false);
    this.toaster.success("User Added Successfully " , "Adding User");
    this.isAlert = false;
    this.userService.sendEmail(email , "Welcome " +username+  " To Attendance App" , "Your Initial Credentials Is \n"+ "Email:"+email+"\n"+"password:"+password).subscribe((data)=>{
      console.log(data)
    },error=>{
      this.isAlert = true;
      this.alertMessage = error.error.responseMessage;
    })  
    this.registerForm.controls["fullname"].setValue("");
    this.registerForm.controls["username"].setValue("");
    this.registerForm.controls["email"].setValue("");
    this.registerForm.controls["password"].setValue("");    
  } ,error=>{
    this.isAlert = true;
    this.alertMessage = error.error.responseMessage;
  })    
  }
  getAllRoles(){
    this.userService.getAllRole().subscribe(roles => {
      this.roles = roles;
    })
  }
  onRoleChange(role:string){
    this.roles.forEach(x=>{
      if(x.role == role)
      {
        x.isSelected = true;
      }
      else{
        x.isSelected = false;
    }

    })
  }

}
