import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { User } from '../Models/User';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-user-managment',
  templateUrl: './user-managment.component.html',
  styleUrls: ['./user-managment.component.scss']
})
export class UserManagmentComponent implements OnInit {
  public userList= new Array<User>();
  constructor(private userService: UserService , private toaster : ToastrService) { }

  ngOnInit(): void {
    this.getAllUser();
  }
  getAllUser(){
    this.userService.getAllUser().subscribe((data)=>{
      console.log(data);
       this.userList = data;
    })
    console.log(this.userList);
  }
  disable (email:string){
    if(confirm("Are you sure to disable "+email)){
      this.userService.disable(email).subscribe((data)=>{
        this.getAllUser();
        this.toaster.success("User disabled successfully" , "Disabling User");
      })
    }
  }
  enable (email:string){
    if(confirm("Are you sure to enable "+email)){
      this.userService.enable(email).subscribe((data)=>{
        this.getAllUser();
        this.toaster.success("User Enabled successfully" , "Enabling User");
      })
    }
  }
}