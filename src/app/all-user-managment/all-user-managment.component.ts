import { Component, OnInit } from '@angular/core';
import { ResponseCode } from '../enums/responseCode';
import { Attendance } from '../Models/Attendance';
import { User } from '../Models/User';
import { AttendanceService } from '../services/attendance.service';
import { UserService } from '../services/user.service';


@Component({
  selector: 'app-all-user-managment',
  templateUrl: './all-user-managment.component.html',
  styleUrls: ['./all-user-managment.component.scss']
})
export class AllUserManagmentComponent implements OnInit {

  public attendanceList= new Array<Attendance>();
  constructor(private userService: UserService , private attendanceService: AttendanceService) { }
  public dt1:Date;
  public selectedMoment = new Date()
  public selectedUser = new String()
  public totalWeekAttendance 
  public selectedMoments = [
    new Date(),
    new Date()
];
public userList= new Array<User>();
  public Result = new Number(0.0)
  public Result1 = new Number(0.0)
  ngOnInit(): void {
    this.getUserList();
    this.getAllUser();
  }
  onChange(data){
  }
  get user():User{
    return JSON.parse(localStorage.getItem("userInfo")) as User 
 }
  clicked(){
    if(this.selectedUser == "Choose User" || this.selectedUser == ""){
      this.selectedUser = this.user.email; 
    }

    this.attendanceService.getSpecific(this.selectedUser, this.selectedMoment).subscribe(res=>
      {
        if(res.responseCode == ResponseCode.OK){
          this.Result = res.dataSet
        }
      })    
  }
  getAllUser(){
    this.userService.getAllUser().subscribe((data)=>{
       this.userList = data;
    })
  }
  clicked1(){
    if(this.selectedUser == "Choose User" || this.selectedUser == ""){
      this.selectedUser = this.user.email; 
    }    
    this.attendanceService.getInterval(this.selectedUser, this.selectedMoments[0] , this.selectedMoments[1]).subscribe(res=>
      {
        if(res.responseCode == ResponseCode.OK){
          this.Result1 = res.dataSet
        }
      })    
  }
  get isAdmin():boolean {
    return this.user.role == "Admin"
  }
  async getUserList(){
   this.attendanceService.lastWeek().subscribe((data)=>{
       this.attendanceList = data;
       let temp=0;
       data.forEach(d=>{
         temp += d.totalTime
       })

    this.totalWeekAttendance = temp
    }
    )
  }

}
