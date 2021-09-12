import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ResponseModel } from '../Models/responseModel';
import { map } from 'rxjs/operators';
import { ResponseCode } from '../enums/responseCode';
import { User } from '../Models/User';
import { Attendance } from '../Models/Attendance';
import { Role } from '../Models/role';

@Injectable({
  providedIn: 'root'
})
export class AttendanceService {
  private readonly baseUrl:string = "http://localhost:8000/api/attendance/"
  constructor(private httpClient:HttpClient) { }
  
  public lastWeek(){
    return this.httpClient.get<ResponseModel>(this.baseUrl+"lastWeek" , {withCredentials : true}).pipe(map(res=>{
      let AttendanceList= new Array<Attendance>();
      if(res.responseCode == ResponseCode.OK){
        console.log(res.dataSet)
        if(res.dataSet){
          res.dataSet.map((x:Attendance)=>{
            AttendanceList.push(new Attendance(x.attendanceDate,x.totalTime,x.dayName))
          })
        }
      }
      return AttendanceList;
    }));
  }
  public setAttendance(From , To){
    const body = {
      From,
      To      
    }
    return this.httpClient.post<ResponseModel>(this.baseUrl+"add" , body ,{withCredentials : true}).subscribe(res =>{
      if(res.responseCode == ResponseCode.OK){
        console.log(res.responseMessage)
      }
    })          
  }
  get user():User{
    return JSON.parse(localStorage.getItem("userInfo")) as User 
 }
  public getSpecific( email ,specificDate){
    const body = {
       email, 
      specificDate      
    }
    return this.httpClient.post<ResponseModel>(this.baseUrl+"specific" , body ,{withCredentials : true})
  }
  public getInterval( email , From , To){
    const body = {
       email, 
      From,
      To      
    }
    return this.httpClient.post<ResponseModel>(this.baseUrl+"interval" , body ,{withCredentials : true})
  }
  public dailyHours(email , From , To){
    const body = {
      email, 
     From,
     To      
   }
    return this.httpClient.post<ResponseModel>(this.baseUrl+"certainDuration" , body,  {withCredentials : true}).pipe(map(res=>{
      let AttendanceList= new Array<Attendance>();
      if(res.responseCode == ResponseCode.OK){
        console.log(res.dataSet)
        if(res.dataSet){
          res.dataSet.map((x:Attendance)=>{
            AttendanceList.push(new Attendance(x.attendanceDate,x.totalTime,x.dayName))
          })
        }
      }
      return AttendanceList;
    }));
  }
  public absent(email , From , To){
    const body = {
      email, 
     From,
     To      
   }
    return this.httpClient.post<ResponseModel>(this.baseUrl+"absent" , body,  {withCredentials : true}).pipe(map(res=>{
      let AttendanceList= new Array<Attendance>();
      if(res.responseCode == ResponseCode.OK){
        console.log(res.dataSet)
        if(res.dataSet){
          res.dataSet.map((x:Attendance)=>{
            AttendanceList.push(new Attendance(x.attendanceDate,x.totalTime,x.dayName))
          })
        }
      }
      return AttendanceList;
    }));
  }
}
