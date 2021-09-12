import { Component, OnInit } from '@angular/core';
import { Attendance } from 'src/app/Models/Attendance';
import { User } from 'src/app/Models/User';
import { AttendanceService } from 'src/app/services/attendance.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-daily-hours',
  templateUrl: './daily-hours.component.html',
  styleUrls: ['./daily-hours.component.scss']
})
export class DailyHoursComponent implements OnInit {
  public selectedMoments = [
    new Date(),
    new Date()
];
public userList= new Array<User>();
public selectedUser = new String()
  constructor(private attendanceService:AttendanceService , private userService:UserService) { }
  public attendanceList= new Array<Attendance>();

  ngOnInit(): void {
    this.getAllUser()
  }
  get user():User{
    return JSON.parse(localStorage.getItem("userInfo")) as User 
 }
 get isAdmin():boolean {
  return this.user.role == "Admin"
}
  clicked1(){
    if(this.selectedUser == "Choose User" || this.selectedUser == ""){
      this.selectedUser = this.user.email; 
    }    
    this.attendanceService.dailyHours(this.selectedUser, this.selectedMoments[0] , this.selectedMoments[1]).subscribe(res=>
      {        
        this.attendanceList = res;
          console.log(res)                  
      })    
  }
  public beatsPerMinuteClasses = {
    downFont: true
};
getAllUser(){
    this.userService.getAllUser().subscribe((data)=>{
      console.log(data);
       this.userList = data;
    })
  }
   isCheater(num):boolean {
     console.log(num)
    return  true
  }
}
