import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { error } from 'protractor';
import { Token } from '@angular/compiler';
import { User } from '../../Models/User';
import { AuthEmployeeService } from '../auth-employee.service';
import { Router, RouterModule, Routes } from '@angular/router';

@Component({
  selector: 'app-login-employee',
  templateUrl: './login-employee.component.html',
  styleUrls: ['./login-employee.component.css']
})
export class LoginEmployeeComponent implements OnInit {


  constructor(private AuthService: AuthEmployeeService, private router: Router) { }

  user: User;

  public username = "";
  public password = "";

  LogIn() : void {
    this.user = {
      username: this.username,
      password: this.password,
    };

    this.AuthService.LoginEmployee(this.user).subscribe(
      () => {
        console.log("Logged in");
        this.router.navigateByUrl('employee-nav/home');

      }
    );
  }


  ngOnInit(): void {
  }

}
