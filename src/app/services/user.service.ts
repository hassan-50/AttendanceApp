import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ResponseModel } from '../Models/responseModel';
import { map } from 'rxjs/operators';
import { ResponseCode } from '../enums/responseCode';
import { User } from '../Models/User';
import { Role } from '../Models/role';
import { AttendanceService } from './attendance.service';


@Injectable({
  providedIn: 'root'
})
export class UserService {
  private readonly baseUrl:string = "http://localhost:8000/api/user/"
  public  From:Date ;
  public  To:Date ;
  constructor(private httpClient:HttpClient , private attendanceService:AttendanceService) { }

  public login(email:string , password:string){
    console.log(new Date);
    const body = {
      Email:email,
      Password:password
    }
     this.From = new Date();
    return this.httpClient.post<ResponseModel>(this.baseUrl+"login",body , {withCredentials : true});
  }
  public register(fullname:string ,username:string ,email:string , password:string , role:string){
    const body = {
      FullName:fullname,
      UserName:username,
      Email:email,
      Password:password,
      Role:role
    }
    return this.httpClient.post<ResponseModel>(this.baseUrl+"register",body , {withCredentials : true});
  }
  async onLogout(){
    this.To = new Date();      
    await this.attendanceService.setAttendance(this.From,this.To)
    this.httpClient.post<ResponseModel>(this.baseUrl+"logout" , {withCredentials : true});
  }
  public getAllUser(){
    return this.httpClient.get<ResponseModel>(this.baseUrl+"GetAllUser" , {withCredentials : true}).pipe(map(res=>{
      let userList= new Array<User>();
      if(res.responseCode == ResponseCode.OK){
        if(res.dataSet){
          res.dataSet.map((x:User)=>{
            console.log(x.role);
            userList.push(new User(x.fullName,x.email,x.userName,x.role , x.checked , x.enabled))
          })
        }
      }
      return userList;
    }));
  }
  public getAllRole(){
    return this.httpClient.get<ResponseModel>(this.baseUrl+"GetRoles" , {withCredentials : true}).pipe(map(res=>{
      let roleList= new Array<Role>();
      if(res.responseCode == ResponseCode.OK){
        if(res.dataSet){
          res.dataSet.map((x:string)=>{
            roleList.push(new Role(x))
          })
        }
      }
      console.log(roleList);
      return roleList;
    }));
  }
  public getUserList(){
    return this.httpClient.get<ResponseModel>(this.baseUrl+"GetUser" , {withCredentials : true}).pipe(map(res=>{
      let userList= new Array<User>();
      if(res.responseCode == ResponseCode.OK){
        if(res.dataSet){
          res.dataSet.map((x:User)=>{
            console.log(x.role);
            userList.push(new User(x.fullName,x.email,x.userName,x.role , x.checked , x.enabled))
          })
        }
      }
      return userList;
    }));
  }
  public changePassword(oldpassword:string , newpassword:string){
    const body = {
      oldpassword:oldpassword,
      newpassword:newpassword      
    }
    return this.httpClient.put<ResponseModel>(this.baseUrl+"ChangePassword",body , {withCredentials : true});
  }
  public sendEmail(ToEmail:string , Subject:string, bodymessage:string){
    var formData: any = new FormData();
    formData.append("ToEmail",ToEmail );
    formData.append("Subject", Subject);
    formData.append("Body", bodymessage);
    return this.httpClient.post<ResponseModel>("http://localhost:8000/api/Mail/Send",formData , {withCredentials : true})
  }
  public addRole(role:string){
    const body = {
      role:role      
    }
    return this.httpClient.post<ResponseModel>(this.baseUrl+"AddRole",body , {withCredentials : true});
  }
  public disable(email:string){
    const body = {
      email      
    }
    return this.httpClient.put<ResponseModel>(this.baseUrl+"disableUser",body , {withCredentials : true});
  }
  public enable(email:string){
    const body = {
      email      
    }
    return this.httpClient.put<ResponseModel>(this.baseUrl+"enableUser",body , {withCredentials : true});
  }
  

}
