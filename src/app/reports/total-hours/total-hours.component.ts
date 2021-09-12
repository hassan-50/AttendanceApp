import { Component, OnInit } from '@angular/core';
import { ResponseCode } from 'src/app/enums/responseCode';
import { User } from 'src/app/Models/User';
import { AttendanceService } from 'src/app/services/attendance.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-total-hours',
  templateUrl: './total-hours.component.html',
  styleUrls: ['./total-hours.component.scss']
})
export class TotalHoursComponent implements OnInit {
  public selectedMoment = new Date()
  public selectedUser = new String()
  public userList= new Array<User>();
  public Result = new Number(0.0)
  constructor(private userService: UserService , private attendanceService: AttendanceService) { }

  ngOnInit(): void {
    this.getAllUser();
  }
  get user():User{
    return JSON.parse(localStorage.getItem("userInfo")) as User 
 }
 get isAdmin():boolean {
  return this.user.role == "Admin"
}
  clicked(){
    console.log(this.selectedUser)
    if(this.selectedUser == "Choose User" || this.selectedUser == ""){
      this.selectedUser = this.user.email; 
    }
    this.attendanceService.getSpecific(this.selectedUser, this.selectedMoment).subscribe(res=>
      {
        if(res.responseCode == ResponseCode.OK){
          console.log(res.dataSet)
          this.Result = res.dataSet
        }
      }) 
  }
  getAllUser(){
    this.userService.getAllUser().subscribe((data)=>{
      console.log(data);
       this.userList = data;
    })
  }
}
