import { Component } from '@angular/core';
import { User } from './Models/User';
import { UserService } from './services/user.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'AttendanceFront';
  constructor( private userService : UserService) { }

  onLogout(){
    localStorage.removeItem("userInfo")
    this.userService.onLogout()
  }
  isUserLogin(){
    const user= localStorage.getItem("userInfo");
    return user && user.length >0;
  }
  get user():User{
     return JSON.parse(localStorage.getItem("userInfo")) as User 
  }

  get isAdmin():boolean {
    return this.user.role == "Admin"
  }
  get isUser():boolean {
    return this.user.role == "User"
  }
  get isChecked():boolean {
    return this.user.checked == 1
  }
}
