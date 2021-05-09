import { Component, OnInit } from '@angular/core';
import { User } from '../../Models/User';
import { AuthEmployeeService } from '../auth-employee.service';
import { Router, RouterModule, Routes } from '@angular/router';

@Component({
  selector: 'app-login-client',
  templateUrl: './login-client.component.html',
  styleUrls: ['./login-client.component.css']
})
export class LoginClientComponent implements OnInit {

  constructor(private AuthService: AuthEmployeeService, private router: Router) { }

  user: User;

  public username = "";
  public password = "";

  LogIn(): void {
    this.user = {
      username: this.username,
      password: this.password,
    };

    this.AuthService.LoginClient(this.user).subscribe(
      () => {
        console.log("Logged in");
        this.router.navigateByUrl('client-nav/home');

      }
    );
  }
  ngOnInit(): void {
  }

}
