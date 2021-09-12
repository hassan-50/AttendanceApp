import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from '../Models/User';

@Component({
  selector: 'app-basic',
  templateUrl: './basic.component.html',
  styleUrls: ['./basic.component.scss']
})
export class BasicComponent implements OnInit {

  constructor(private router :Router) { }

  ngOnInit(): void {
    if(this.user != null){
      this.router.navigate(["/alluser-managment"]);
    }
    else{
      this.router.navigate(["/login"]);
    }

  }
  get user():User{
    return JSON.parse(localStorage.getItem("userInfo")) as User 
 }
}
